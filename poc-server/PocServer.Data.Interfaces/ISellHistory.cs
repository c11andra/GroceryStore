using System;

namespace PocServer.Data.Interfaces
{
    public interface ISellHistory
    {
        int Id { get; set; }
        int Quantity { get; set; }
        double SellingPrice { get; set; }
        DateTime InsertUtc { get; set; }
    }
}
