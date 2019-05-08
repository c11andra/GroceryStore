using System;
using System.Collections.Generic;
using PocServer.Data.Interfaces;
using PocServer.StoreManagement.Interfaces;

namespace PocServer.WebApi
{
    public class SellRequest : ISellRequest
    {
        
        public IEnumerable<IProduct> Products { get; set; }
        public UserTypeEnum UserType {get;set;}
    }
}
