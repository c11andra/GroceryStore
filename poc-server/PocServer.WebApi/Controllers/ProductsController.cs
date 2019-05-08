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
            var res = _storeManager.GetProducts();
            if (res != null) return Ok(res);
            return NotFound();

        }

        // POST api/values
        [HttpPost("sell")]
        public ActionResult<string> Post(
            [FromBody] SellRequest sellRequest)
        {
            var response = _storeManager.Sell(sellRequest);
            if (response.Error == null)
                return Ok();
            else
                return NotFound();
        }

        //  // POST api/values
        // [HttpGet("sell/Report")]
        // public ActionResult<IReport> GetSellReport(string FromDate, string ToData)
        // {
        //     var response = _storeManager.Sell(sellRequest);
        //     if (response.Error == null)
        //         return Ok();
        //     else
        //         return NotFound();
        // }

    }
}
