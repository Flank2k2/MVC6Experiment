using MVC6Experiment.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MVC6Experiment.Repository
{
    public interface IRepository
    {
        IEnumerable<Template> GetAllTemplates();
        Template GetTemplate(String name);
        String SaveTemplate(Template template);


        IEnumerable<Client> GetAllClients();
        IEnumerable<Client> GetAllClientsByHostname(String hostname);
        Client GetClient(String hostname);
        IEnumerable<Client> SearchClients(String hostname = "", String addressip = "", String name = "");
    }
}
