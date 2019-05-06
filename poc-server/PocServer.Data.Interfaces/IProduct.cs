using System;

namespace PocServer.Data.Interfaces
{
    public interface IProduct
    {
        int Id { get; set; }
        string Name { get; set; }
        int Quantity { get; set; }
        double Price { get; set; }
        ICategory Category { get; set; }
    }
}
