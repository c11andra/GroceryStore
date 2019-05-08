using System;
using System.Collections.Generic;
using PocServer.Data.Interfaces;
using PocServer.StoreManagement.Interfaces;

namespace PocServer.StoreManagement
{
    public class SellRequestDto : ISellRequest
    {
        public IEnumerable<IProduct> Products { get; set; }
        public UserTypeEnum UserType { get; set; }
    }
}
