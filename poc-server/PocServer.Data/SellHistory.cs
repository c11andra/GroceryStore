using System;
using PocServer.Data.Interfaces;

namespace PocServer.Data
{
    public class SellHistory: ISellHistory
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public double SellingPrice { get; set; }
        public DateTime InsertUtc { get; set; }

        public int ProductId {get;set;}
    }
}
