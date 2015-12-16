// <copyright file="Program.cs" company="PlaceholderCompany">
// Copyright (c) PlaceholderCompany. All rights reserved.
// </copyright>

namespace ListenerLiveTest.Client
{
    using System;
    using System.Collections.Generic;
    using System.Dynamic;
    using System.Threading.Tasks;
    using AMSLLC.Listener.Client;
    using AMSLLC.Listener.Client.Message;
    using AMSLLC.Listener.Shared;
    using Communication;
    using Message;
    using Newtonsoft.Json;
    using Serilog;

    class Program
    {
        static void Main(string[] args)
        {
            const int parallelCount = 3;

            var log = new LoggerConfiguration().ReadFrom.AppSettings().CreateLogger();
            Log.Logger = log;
            try
            {
                if (args == null || args.Length == 0)
                {
                    Log.Error("Please specify request type");
                }
                else
                {
                    var key = args[0];
                    switch (key.ToUpperInvariant())
                    {
                        case "-CLIENT":
                            UseClient(int.Parse(args[1]));
                            break;
                        case "-MANAGER":
                            UseManager(parallelCount, int.Parse(args[1]));
                            break;
                        case "-BATCHCLIENT":
                            UseClientForBatch();
                            break;
                        case "-QUERY":
                            var filter = new TransactionFilter();
                            filter.StatusTypes.Add(TransactionStatusType.Failed);
                            QueryData(filter);
                            break;
                        case "-EXPLICIT":
                            UseExplicit();
                            break;
                        case "-TEST":
                            UseClient(20);
                            UseManager(parallelCount, 20);
                            UseClientForBatch();
                            var f = new TransactionFilter();
                            f.StatusTypes.Add(TransactionStatusType.Failed);
                            QueryData(f);
                            UseExplicit();
                            break;
                    }

                }
            }
            catch (Exception exc)
            {
                Log.Logger.Error("Closing due to an error: {0}", exc.Message);
            }
            finally
            {
                Console.WriteLine("Wrapping up...");
                GC.Collect();
                GC.WaitForPendingFinalizers();
                Console.WriteLine("Finished.");
                //Console.ReadLine();
            }
        }

        private static void UseClient(int totalRequests)
        {
            var client = new ListenerClient(AppConfig.ListenerUrl);
            try
            {
                for (int i = 0; i < totalRequests; i++)
                {
                    client.ProcessDeviceTestResult(new DeviceTestResultMessage
                    {
                        CompanyId = "SomeCompany",
                        EquipmentNumber = Guid.NewGuid().ToString(),
                        EquipmentType = "EM",
                        TestDate = DateTime.Now
                    });
                }
            }
            finally
            {
                client.Dispose();
            }
        }

        private static void UseExplicit()
        {
            var client = new ListenerClient(AppConfig.ListenerUrl);
            try
            {
                Log.Logger.Information("Opening transaction");
                var t = client.OpenTransactionAsync(new InstallMeterRequestMessage { EntityKey = Guid.NewGuid().ToString("D") });
                t.Wait();
                var obj = JsonConvert.DeserializeObject<ExpandoObject>(t.Result) as IDictionary<string, object>;
                var transactionKey = (string)obj["value"];
                Log.Logger.Information("Processing {0}", transactionKey);
                var t1 = client.ProcessTransactionAsync(transactionKey);
                t1.Wait();
                Log.Logger.Information("Succeeding {0}", transactionKey);
                var t2 = client.SucceedTransactionAsync(transactionKey);
                t2.Wait();
            }
            finally
            {
                client.Dispose();
            }
        }

        private static void QueryData(TransactionFilter filter)
        {
            var client = new ListenerClient(AppConfig.ListenerUrl);
            try
            {

                var result = client.SearchTransactions(filter);
                foreach (var transactionInfoResponseMessage in result)
                {
                    Log.Logger.Information("Entity Key: {0}, Created Date: {1}, Entity Category: {2}, Message: {3}",
                        transactionInfoResponseMessage.EntityKey, transactionInfoResponseMessage.CreatedDate,
                        transactionInfoResponseMessage.EntityCategory, transactionInfoResponseMessage.Message);
                }
            }
            finally
            {
                client.Dispose();
            }
        }

        private static void UseClientForBatch()
        {
            var client = new ListenerClient(AppConfig.ListenerUrl);
            try
            {
                client.ProcessBatch(new BatchAcceptedMessage { BatchNumber = "B1" });
            }
            finally
            {
                client.Dispose();
            }
        }

        private static void UseManager(int parallelCount, int totalRequests)
        {
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
                            data.Add(new OpenTransactionRequestWrapper<OpenTransactionRequestMessage>(new OpenTransactionRequestMessage("EM", "Install")
                            {
                                EntityKey = Guid.NewGuid().ToString("D")
                            }));
                        }

                        await manager.Run(new Uri($"{AppConfig.ListenerUrl}/Open"), data.ToArray());
                        Log.Logger.Information("Manager completed");
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
                    Console.WriteLine("{0}{1}{2}", innerException.Message, Environment.NewLine,
                        innerException.StackTrace);
                }
                throw;
            }
        }
    }
}