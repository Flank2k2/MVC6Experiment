using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MVC6Experiment.Model;

namespace MVC6Experiment.Repository
{
    public class SimpleRepository : IRepository
    {
        public IEnumerable<Client> GetAllClients()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client> GetAllClientsByHostname(string hostname)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Template> GetAllTemplates()
        {
            throw new NotImplementedException();
        }

        public Client GetClient(string hostname)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Client> SearchClients(string hostname = "", string addressip = "", string name = "")
        {
            throw new NotImplementedException();
        }
    }
}
