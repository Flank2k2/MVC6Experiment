using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6Experiment.Model;
using System.Collections.Concurrent;
using Serilog;
using System.IO;
using System.Xml.Serialization;
using Newtonsoft.Json;

namespace MVC6Experiment.Repository
{
    public class SimpleRepository : IRepository
    {
        private readonly ConcurrentDictionary<String, Client> _clients;
        private readonly ConcurrentDictionary<String, Template> _templates;
        private readonly String _directory;

        public SimpleRepository(String templateDirectory, String clientFile)
        {
            _clients = new ConcurrentDictionary<string, Client>();
            if (File.Exists(clientFile))
            {
                Log.Information("Parse Client data {tmplFile}", clientFile);
                var data = File.ReadAllLines(clientFile);
                foreach (var line in data.Skip(1))
                {
                    var tokens = line.Split(',');
                    var client = new Client()
                    {
                        Hostname = tokens[0],
                        AddressIP = tokens[1],
                        DisplayName = tokens[2],
                        Environment = tokens[3],
                        URL = tokens[4],
                        DetailsURL = tokens[5],
                        IsHosted = bool.Parse(tokens[6]),
                        Id = tokens[7]
                    };

                    Log.Information("Client found: {tmplName}", client.Id);
                    _clients.TryAdd(client.Id, client);
                }
            }
            else
            {
                Log.Error("Client File is missing {clientFile}", clientFile);
            }

            _templates = new ConcurrentDictionary<string, Template>();
            if (Directory.Exists(templateDirectory))
            {
                _directory = templateDirectory;

                Log.Information("Load template data {tmplDirectory}", templateDirectory);
                foreach (var tmplFile in Directory.GetFiles(templateDirectory, "*.json"))
                {
                    try
                    {
                        Log.Information("Parse {tmplFile}", tmplFile);
                        var tempalte = JsonConvert.DeserializeObject<Template>(File.ReadAllText(tmplFile));

                        _templates.TryAdd(tempalte.Id, tempalte);  //Let's see what happens .... 
                        Log.Information("Template found:  {tmplName}", tempalte.Id);
                    }
                    catch (Exception ex)
                    {
                        Log.Error(ex, "Error parsing {tmplFile}", tmplFile);
                    }
                }
            }
            else
            {
                Log.Error("Template directory is missing {tmplDirectory}", templateDirectory);
            }

        }

        public Client GetClient(string id)
        {
            if (!_clients.ContainsKey(id))
                return null;

            return _clients[id];
        }
        public IEnumerable<Client> GetAllClients()
        {
            return _clients.Values;
        }
        public IEnumerable<Client> GetAllClientsByHostname(string hostname)
        {
            return _clients.Values.Where(cl => cl.Hostname.ToLower() == hostname.ToLower());
        }
        public IEnumerable<Client> SearchClients(string hostname = "", string addressip = "", string name = "")
        {
            return _clients.Values.Where(cl => cl.Hostname.ToLower() == hostname.ToLower() ||
                                               cl.AddressIP.ToLower() == addressip.ToLower() ||
                                               cl.DisplayName.ToLower() == name.ToLower());

        }

        public IEnumerable<Template> GetAllTemplates()
        {
            return _templates.Values;
        }
        public Template GetTemplate(String name)
        {
            if (!_templates.ContainsKey(name))
                return null;

            return _templates[name];
        }

        public string SaveTemplate(Template template)
        {
            var id = Guid.NewGuid();
            template.Id = id.ToString();

            //ADD VALIDATION !!

            var filename = Path.Combine(_directory, id + ".json");
            var data = JsonConvert.SerializeObject(template);

            Log.Information("Write template to file:  {tmplName}", template.Id);
            File.WriteAllText(filename, data);

            Log.Information("Template saved:  {tmplName}", template.Id);
            _templates.TryAdd(template.Id, template);  //Let's see what happens .... 

            return id.ToString();
        }
    }
}
