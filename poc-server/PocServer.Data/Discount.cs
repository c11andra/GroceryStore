using System;
using PocServer.Data.Interfaces;
namespace PocServer.Data
{
    internal class Discount : IDiscount
    {
        public DiscountTypeEnum DiscountType { get; set; }
        public double Amount { set; get; }

        public int ProductId { get; set; }
        public int UserType { get; set; }
        public DateTime InsertUtc { get; set; }
        public DateTime ValidTill { get; set; }
        public int Max { get; set; }

    }
}