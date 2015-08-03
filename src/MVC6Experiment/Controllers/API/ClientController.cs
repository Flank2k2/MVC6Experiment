using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using MVC6Experiment.Model;
using MVC6Experiment.Repository;

// For more information on enabling Web API for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MVC6Experiment.Controllers.API
{
    [Route("api/[controller]")]
    public class ClientsController : Controller
    {
        private readonly IRepository _repo;

        public ClientsController(IRepository repository)
        {
            _repo = repository;
        }

        //[HttpGet]
        //public IEnumerable<Client> GetAll()
        //{
        //    return _repo.GetAllClients();
        //}

        [HttpGet]
        public IEnumerable<Client> Search(string hostname = "", string addressip = "", string name = "")
        {
            var result = _repo.SearchClients(hostname, addressip, name);

            return result;
       }


        [HttpGet("{hostname}")]
        public IActionResult Get(string hostname)
        {
            var client = _repo.GetClient(hostname);

            if (client == null)
                return HttpNotFound();

            return new ObjectResult(client);
        }
        
        

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
            throw new NotImplementedException("Ahaha Where are you going !?!");
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(string id, [FromBody]string value)
        {
            throw new NotImplementedException("Ahaha Where are you going !?!");
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(string id)
        {
            throw new NotImplementedException("Ahaha Where are you going !?!");
        }
    }
}
