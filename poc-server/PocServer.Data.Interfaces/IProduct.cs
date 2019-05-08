using System;

namespace PocServer.Data.Interfaces
{
    public interface IProduct
    {
        int Id { get;  }
        string Name { get;  }
        int Quantity { get;  }
        double Price { get;  }
        ICategory Category { get;  }
    }
}
