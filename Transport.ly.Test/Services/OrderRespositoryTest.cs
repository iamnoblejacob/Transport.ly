using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Transport.ly.Repositories;
using System.Linq;
using Transport.ly.Services;

namespace Transport.ly.Test.Services
{
    public class ScheduleServiceTest
    {
        [Fact]
        public void ScheduleOrdersToFlights()
        {
            // Arrange
            var collection = new ServiceCollection();
            collection.AddScoped<IOrderRespository, OrderRespositoryJson>();
            collection.AddScoped<IFlightScheduleRepository, FlightScheduleRepositoryJson>();
            collection.AddScoped<IScheduleService, ScheduleService>();

            var serviceProvider = collection.BuildServiceProvider();
            var scheduleService = serviceProvider.GetService<IScheduleService>();

            // Act
            var packagesAssigned = scheduleService.ScheduleOrdersToFlights();

            //Assert
            Assert.True(packagesAssigned.Any());
        }
    }
}
