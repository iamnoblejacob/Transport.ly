using System.Collections.Generic;
using Transport.ly.Models;

namespace Transport.ly.Repositories
{
    public interface IOrderRespository
    {
        Dictionary<string, Flight> LoadPackageOrders();
    }
}
