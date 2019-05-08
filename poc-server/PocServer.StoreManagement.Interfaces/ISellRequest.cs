using System.Collections.Generic;
using PocServer.Data.Interfaces;

namespace  PocServer.StoreManagement.Interfaces
{
    public interface ISellRequest
    {
        UserTypeEnum UserType{get;set;}
        IEnumerable<IProduct> Products { get; set; }
    }

}