using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebAPI.Shippers.Models;

namespace WebAPI.Shippers.Controllers
{
    public class TransportadorasController : ApiController
    {
        // GET api/<controller>
        public IEnumerable<Transportadora> Get()
        {
            var db = new NorthwindEntities();
            return db.Transportadoras.ToList();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<controller>
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}