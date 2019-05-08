using System;
using System.Collections.Generic;

namespace PocServer.Data.Interfaces
{
    public interface IDataAccess
    {
        IEnumerable<IProduct> GetProducts();
        IEnumerable<IDiscount> GetDiscount(IProduct product, UserTypeEnum userType);
        int GetProductQuantity(int id);
        void UpdateProductQuantity(IProduct product);
        bool InsertSellHistory(ISellHistory sellHistory);
        IEnumerable<ISellHistory> GetSellHistoryOfToday();
        IEnumerable<ISellHistory> GetSellHistory(string fromdate, string todate);
        IProduct GetProductById(int productId);
    }
}
