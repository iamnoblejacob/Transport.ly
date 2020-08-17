using Microsoft.Extensions.DependencyInjection;
using System;
using Transport.ly.Repositories;
using Transport.ly.Services;

namespace Transport.ly
{
    class Program
    {
        static void Main(string[] args)
        {
            // Add DI
            var collection = new ServiceCollection();
            collection.AddScoped<IOrderRespository, OrderRespositoryJson>();
            collection.AddScoped<IFlightScheduleRepository, FlightScheduleRepositoryJson>();
            collection.AddScoped<IScheduleService, ScheduleService>();

            var serviceProvider = collection.BuildServiceProvider();

            var schedulerService = serviceProvider.GetService<IScheduleService>();

            var scheduledOrders = schedulerService.ScheduleOrdersToFlights();

            schedulerService.PrintScheduledOrders(scheduledOrders);

            // Dispose
            if (serviceProvider is IDisposable)
            {
                ((IDisposable)serviceProvider).Dispose();
            }
        }
    }
}
