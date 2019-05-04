using System;
using System.Collections.Generic;
using PocServer.Data.Interfaces;

namespace PocServer.StoreManagement.Interfaces
{
    public interface IStoreManager
    {
        IEnumerable<IProduct> GetProducts();
    }
}
