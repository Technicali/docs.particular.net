using System;
using NServiceBus;
using NServiceBus.Saga;

namespace NHibernateSagaMapping
{
    class Program
    {
        static void Main(string[] args)
        {
            var busConfig = new BusConfiguration();
            busConfig.UsePersistence<NHibernatePersistence>();
            busConfig.UseTransport<SqlServerTransport>();

            var bus = Bus.Create(busConfig).Start();

            Console.WriteLine("Generating saga instances.");

            bus.SendLocal(new SagaStarter());
            Console.WriteLine("Press <enter> to exit.");
            Console.ReadLine();
        }
    }

}
