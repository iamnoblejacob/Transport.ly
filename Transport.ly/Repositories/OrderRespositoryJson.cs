using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Transport.ly.Models;

namespace Transport.ly.Repositories
{
    public class OrderRespositoryJson: IOrderRespository
    {
        public Dictionary<string, Flight> LoadPackageOrders()
        {
            using var sr = File.OpenText(@"Data\orders.json")??
                throw new InvalidOperationException("Failed to load order data");

            return JsonConvert.DeserializeObject<Dictionary<string, Flight>>(sr?.ReadToEnd());
        }        
    }
}
