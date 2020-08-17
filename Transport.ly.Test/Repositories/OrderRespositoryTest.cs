using System;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Transport.ly.Repositories;
using System.Linq;

namespace Transport.ly.Test.Repositories
{
    public class OrderRespositoryTest
    {
        [Fact]
        public void LoadFlightScheduleTest()
        {
            // Arrange
            var collection = new ServiceCollection();
            collection.AddScoped<IOrderRespository, OrderRespositoryJson>();
            collection.AddScoped<IFlightScheduleRepository, FlightScheduleRepositoryJson>();

            var serviceProvider = collection.BuildServiceProvider();
            var orderRepository = serviceProvider.GetService<IOrderRespository>();

            // Act
            var orders = orderRepository.LoadPackageOrders();

            //Assert
            Assert.True(orders.Any());

        }
    }
}
