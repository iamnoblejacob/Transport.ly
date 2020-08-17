using System;
using System.Collections.Generic;
using System.Text;

namespace Transport.ly.Models
{
    public class Flight
    {
        public int Day { get; set; }
        public int FlightNumber { get; set; }
        public string Source { get; set; } = "YUL";
        public string Destination { get; set; }
    }
}
