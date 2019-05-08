using System;
using PocServer.Data.Interfaces;

namespace PocServer.StoreManagement.Interfaces
{
    public interface ISoldProduct : IProduct
    {
        double SellingPrice { get; set; }
        double Discount { get; set; }
    }
}
