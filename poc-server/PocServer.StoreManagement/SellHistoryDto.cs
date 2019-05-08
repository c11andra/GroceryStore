using System;
using PocServer.Data.Interfaces;

namespace PocServer.StoreManagement
{
    public class SellHistoryDto:ISellHistory
    {

        public int Id {get;set;}
        public int Quantity {get;set;}
        public double SellingPrice {get;set;}
        public DateTime InsertUtc {get;set;}

        public int ProductId {get;set;}
    }
}