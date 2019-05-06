using System;
using PocServer.Data.Interfaces;
namespace PocServer.Data
{
    public class Product : IProduct
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
        public ICategory Category { get; set; }
        public double Price { get; set; }
    }
}
