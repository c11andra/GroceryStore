using System;
using System.Linq;
using System.Collections.Generic;
using PocServer.StoreManagement.Interfaces;

namespace PocServer.StoreManagement
{
    public class SellReportResponse:ISellReportResponse
    {
        public IEnumerable<ISoldProduct> Products {get;set;}
        public double TotalPrice
        {
            get{
                return Products.Sum(p=>p.SellingPrice);
            }
        }

        public string Error {get;set;}
    }
}
