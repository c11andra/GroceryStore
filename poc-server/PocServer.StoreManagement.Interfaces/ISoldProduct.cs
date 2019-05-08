using System;
using PocServer.Data.Interfaces;

namespace PocServer.StoreManagement.Interfaces
{
    public interface ISoldProduct : IProduct
    {
        double SellingPrice { get;  }
        double Discount { get;  }
    }
}
