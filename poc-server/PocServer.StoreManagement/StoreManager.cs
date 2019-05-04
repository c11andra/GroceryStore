using System;
using System.Collections.Generic;
using PocServer.StoreManagement.Interfaces;
using PocServer.Data.Interfaces;

namespace PocServer.StoreManagement
{
    public class StoreManager:IStoreManager
    {
        private readonly IDataAccess _dataAccess;

        public StoreManager(IDataAccess dataAccess)
        {
            _dataAccess = dataAccess;
        }
        public IEnumerable<IProduct> GetProducts()
        {
            return _dataAccess.GetProducts();
        }
    }
}
