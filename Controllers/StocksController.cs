using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Stocks_Core_API.Controllers
{
    [Route("api/[controller]")]
    public class StocksController : Controller
    {
        // GET api/stocks
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/stocks/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/stocks
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/stocks/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/stocks/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
