using System;
using System.Collections.Generic;

namespace PocServer.StoreManagement.Interfaces
{
    public interface ISellReportResponse : IBaseResponse
    {
        IEnumerable<ISoldProduct> Products{get;set;}
        double TotalPrice {get;}
    }
}
