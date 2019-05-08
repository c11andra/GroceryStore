using System;
using System.Collections.Generic;
using PocServer.Data.Interfaces;
using PocServer.StoreManagement.Interfaces;

namespace PocServer.StoreManagement
{
    public class SellResponse : ISellResponse
    {

        public SellResponse()
        {
            this.Error = null;
        }
        public string Error { get; set; }
        public IEnumerable<ISoldProduct> Products
        {
            get
            {
                return SoldProducts;
            }
        }


        public IList<ISoldProduct> SoldProducts { get; set; }
    }
}
