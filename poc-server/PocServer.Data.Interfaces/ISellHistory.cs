using System;

namespace PocServer.Data.Interfaces
{
    public interface ISellHistory
    {
        int Id { get;  }
        int Quantity { get;  }
        double SellingPrice { get;  }
        DateTime InsertUtc { get;  }

        int ProductId{get;} 
    }
}
