using System.Collections.Generic;
using Transport.ly.Models;

namespace Transport.ly.Services
{
    public interface IScheduleService
    {
        Dictionary<string, Flight> ScheduleOrdersToFlights();
        void PrintScheduledOrders(Dictionary<string, Flight> orders);

    }
}
