using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Transport.ly.Models;

namespace Transport.ly.Repositories
{
    public class FlightScheduleRepositoryJson:IFlightScheduleRepository
    {
        public IEnumerable<Flight> LoadFlightSchedule()
        {
            using var sr = File.OpenText(@"Data\flights.json") ??
                throw new InvalidOperationException("Failed to load order data");
            var json = sr?.ReadToEnd();

            var flights = JsonConvert.DeserializeObject<IEnumerable<Flight>>(json);

            // Set Flight Number
            var flightCounter = 1;
            foreach (var flight in flights)
                flight.FlightNumber = flightCounter++;

            return flights;
        }

        public void PrintFlightSchedule(IEnumerable<Flight> flightsSchedule)
        {
            foreach (var flight in flightsSchedule)
                    Console.WriteLine(string.Format("Flight: {0}, departure: {1}, arrival: {2}, day: {3}",
                        flight.FlightNumber.ToString(), flight.Source, flight.Destination, flight.Day.ToString()));
        }
    }
}
