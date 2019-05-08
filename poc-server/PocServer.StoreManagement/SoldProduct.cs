using System;
using PocServer.Data.Interfaces;
using PocServer.StoreManagement.Interfaces;

namespace PocServer.StoreManagement
{
    public class SoldProduct : ISoldProduct
    {
        public double SellingPrice {get;set;}
        public double Discount {get;set;}
        public int Id {get;set;}
        public string Name {get;set;}
        public int Quantity {get;set;}
        public double Price {get;set;}
        public ICategory Category {get;set;}
    }
}
