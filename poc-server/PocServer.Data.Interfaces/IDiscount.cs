using System;

namespace PocServer.Data.Interfaces
{
    public interface IDiscount
    {
        DiscountTypeEnum DiscountType { get;  }
        double Amount { get;  }

        int ProductId { get;  }
        int UserType { get;  }
        DateTime InsertUtc { get;  }
        DateTime ValidTill { get;  }
        int Max { get;  }
    }
}
