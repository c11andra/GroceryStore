using System;
using System.Collections.Generic;

namespace PocServer.Data.Interfaces
{
    public interface IDataAccess
    {
        IEnumerable<IProduct> GetProducts();
    }
}
