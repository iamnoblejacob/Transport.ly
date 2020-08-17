using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using Transport.ly.Models;

namespace Transport.ly.Repositories
{
    public interface IFlightScheduleRepository
    {
        IEnumerable<Flight> LoadFlightSchedule();

        void PrintFlightSchedule(IEnumerable<Flight> flightsSchedule);
    }
}
