using System;

namespace PocServer.Data.Interfaces
{
    public interface IDiscount
    {
        DiscountTypeEnum DiscountType { get; set; }
        double Amount { set; get; }

        int ProductId { get; set; }
        int UserType { get; set; }
        DateTime InsertUtc { get; set; }
        DateTime ValidTill { get; set; }
        int Max { get; set; }
    }
}
