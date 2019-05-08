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
 
        [HttpGet]
        public ActionResult<IEnumerable<IProduct>> Get()
        {
            var res = _storeManager.GetProducts();
            if (res != null) return Ok(res);
            return NotFound();

        }

        
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

        [HttpGet("sell/Report/today")]
        public ActionResult<ISellReportResponse> GetReportOfToday()
        {
            var response = _storeManager.GetReportOfToday();
            if (response.Error == null)
                return Ok();
            else
                return NotFound();
        }

         
        [HttpGet("sell/Report/{fromdate}/{todate}")]
        public ActionResult<ISellReportResponse> GetSellReport([FromQuery]string fromdate, [FromQuery]string todate)
        {
            var response = _storeManager.GetSellReport(fromdate, todate);
            if (response.Error == null)
                return Ok();
            else
                return NotFound();
        }

    }
}
