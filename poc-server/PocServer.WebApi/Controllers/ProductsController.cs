using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using PocServer.StoreManagement.Interfaces;
using PocServer.Data.Interfaces;

namespace PocServer.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IStoreManager _storeManager;

        public ProductsController(IStoreManager storeManager)
        {
            _storeManager = storeManager;
        }
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<IProduct>> Get()
        {
            return Ok(_storeManager.GetProducts());
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
