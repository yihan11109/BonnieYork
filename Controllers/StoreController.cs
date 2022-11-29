using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace BonnieYork.Controllers
{
    public class StoreController : ApiController
    {
        // GET: api/Store
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Store/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Store
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Store/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Store/5
        public void Delete(int id)
        {
        }
    }
}
