using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Transport.ly.Models;
using Transport.ly.Repositories;

namespace Transport.ly.Services
{
    public class ScheduleService : IScheduleService
    {
        private readonly IOrderRespository _orderRespository;
        private readonly IFlightScheduleRepository _flightRespository;
        private const int MaxFlightCapacity = 20;
        public ScheduleService(IOrderRespository orderRespository, IFlightScheduleRepository flightRespository)
        {
            _orderRespository = orderRespository;
            _flightRespository = flightRespository;
        }

        public Dictionary<string, Flight> ScheduleOrdersToFlights()
        {
            // Load flight schedule
            var flightsScheduled = _flightRespository.LoadFlightSchedule();

            _flightRespository.PrintFlightSchedule(flightsScheduled);

            // Load orders
            var orders = _orderRespository.LoadPackageOrders();

            // Allocate packages to available flights
            return AllocatePackagesToFlights(orders, flightsScheduled);
        }

        public void PrintScheduledOrders(Dictionary<string, Flight> orders)
        {
            foreach (var order in orders)
                Console.WriteLine(order.Value == null
                    ? string.Format("order: {0}, flightNumber: not scheduled", order.Key)
                    : string.Format("order: {0}, flightNumber: {1}, departure: {2}, arrival: {3}, day: {4}",
                    order.Key, order.Value.FlightNumber, order.Value.Source, order.Value.Destination, order.Value.Day));
        }

        private Dictionary<string, Flight> AllocatePackagesToFlights(Dictionary<string, Flight> orders, IEnumerable<Flight> flightsScheduled)
        {
            var flightInventoryStat = new Dictionary<int, int>();

            var finalAllocatedOrders = new Dictionary<string, Flight>();

            foreach (var order in orders)
            {
                var flightAssigned = GetFlightToAssignPackage(flightInventoryStat, order.Value.Destination, flightsScheduled);

                finalAllocatedOrders.Add(order.Key, flightAssigned);
            }

            return finalAllocatedOrders;
        }

        private Flight GetFlightToAssignPackage(Dictionary<int, int> flightInventoryStat, string packageDestination, IEnumerable<Flight> flightsScheduled)
        {
            foreach(var flight in flightsScheduled.Where(s => s.Destination == packageDestination))// Flights to package destination
            {
                // No packages assigned to this flight
                if (!flightInventoryStat.ContainsKey(flight.FlightNumber))
                {
                    flightInventoryStat.Add(flight.FlightNumber, 1);
                    return flight;
                }
                // If flight not meet capacity yet
                else if(flightInventoryStat.TryGetValue(flight.FlightNumber, out var packageCount) &&
                    packageCount < MaxFlightCapacity)
                {
                    flightInventoryStat[flight.FlightNumber] = packageCount + 1;
                    return flight;
                }
            }

            return null;
        }
    }
}
