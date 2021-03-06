﻿using System;
using NServiceBus;

class Program
{
    static void Main()
    {
        BusConfiguration busConfiguration = new BusConfiguration();
        busConfiguration.EndpointName("Samples.Encryption.Endpoint2");
        busConfiguration.RijndaelEncryptionService();
        busConfiguration.UsePersistence<InMemoryPersistence>();
        using (Bus.Create(busConfiguration).Start())
        {
            Console.WriteLine("Press any key to exit");
            Console.ReadLine();
        }
    }
}