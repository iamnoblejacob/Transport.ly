using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Transport.ly.Repositories;
using System.Linq;

namespace Transport.ly.Test.Repositories
{
    public class FlightScheduleRepositoryTest
    {
        [Fact]
        public void LoadFlightScheduleTest()
        {
            // Arrange
            var collection = new ServiceCollection();
            collection.AddScoped<IOrderRespository, OrderRespositoryJson>();
            collection.AddScoped<IFlightScheduleRepository, FlightScheduleRepositoryJson>();

            var serviceProvider = collection.BuildServiceProvider();
            var flightScheduleRepository = serviceProvider.GetService<IFlightScheduleRepository>();

            // Act
            var flights = flightScheduleRepository.LoadFlightSchedule();

            //Assert
            Assert.True(flights.Any());

        }
    }
}
