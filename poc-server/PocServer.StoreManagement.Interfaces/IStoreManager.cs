﻿using System;
using System.Collections.Generic;
using PocServer.Data.Interfaces;
namespace PocServer.StoreManagement.Interfaces
{
    public interface IStoreManager
    {
        IEnumerable<IProduct> GetProducts();
        ISellResponse Sell(ISellRequest sellRequest);
        ISellReportResponse GetReportOfToday();
        ISellReportResponse GetSellReport(string fromdate, string todate);
    }
}
