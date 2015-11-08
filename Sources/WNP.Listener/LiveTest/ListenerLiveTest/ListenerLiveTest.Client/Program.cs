﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ListenerLiveTest.Client
{
    using Communication;
    using Message;
    using Serilog;

    class Program
    {
        static void Main(string[] args)
        {
            const int parallelCount = 5;
            const int totalRequests = 100;
            var log = new LoggerConfiguration().ReadFrom.AppSettings().CreateLogger();
            Log.Logger = log;

            CommunicationManager manager = new CommunicationManager(parallelCount);
            try
            {
                Task.Run(async () =>
                                {
                                    try
                                    {
                                        List<object> data = new List<object>();
                                        for (int i = 0; i < totalRequests; i++)
                                        {
                                            data.Add(new OpenTransactionRequestMessage("ElectricMeters", "Install")
                                            {
                                                EntityKey = Guid.NewGuid().ToString("D")
                                            });
                                        }

                                        await manager.Run(new Uri("http://localhost:9000/listener/Open"), data.ToArray());
                                    }
                                    catch (Exception exc)
                                    {
                                        var e = exc;
                                        while (e != null)
                                        {
                                            Console.WriteLine("{0}{1}{2}", e.Message, Environment.NewLine, e.StackTrace);
                                            e = e.InnerException;
                                        }
                                        throw;
                                    }
                                }).Wait();
            }
            catch (AggregateException exc)
            {
                var e = exc.Flatten();
                Console.WriteLine("{0}{1}{2}", e.Message, Environment.NewLine, e.StackTrace);
                foreach (var innerException in e.InnerExceptions)
                {
                    Console.WriteLine("{0}{1}{2}", innerException.Message, Environment.NewLine, innerException.StackTrace);
                }
                throw;
            }

            Console.WriteLine("Press Enter to exit...");
            Console.ReadLine();
        }
    }
}