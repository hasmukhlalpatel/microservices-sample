using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server.Features;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace OcelotApiGateways
{
    //https://docs.microsoft.com/pl-pl/dotnet/architecture/microservices/multi-container-microservice-net-applications/implement-api-gateways-with-ocelot
    //https://altkomsoftware.pl/en/blog/building-api-gateways-with-ocelot/
    //https://ocelot.readthedocs.io/en/latest/index.html
    public class Program
    {
        public static void Main(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Path.Combine("configuration", $"hostsettings.json"), optional: true)
                .AddCommandLine(args)
                .Build();

            //CreateWebHostBuilder(args).Build().Run();
            var builder = CreateWebHostBuilder(args);
            var host = builder.Build();

            foreach (var address in host.ServerFeatures
                .Get<IServerAddressesFeature>()
                .Addresses)
            {
                Console.WriteLine($"listening on {address}");
            }

            host.Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
            .ConfigureLogging((hostingContext, logging)=> 
            {
                var env = hostingContext.HostingEnvironment;
                Console.WriteLine(env.EnvironmentName);
                if (env.IsDevelopment())
                {
                    logging.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
                    logging.AddConsole();
                    logging.AddDebug();
                    logging.AddEventSourceLogger();
                    logging.SetMinimumLevel(LogLevel.Debug);
                }
                else
                {
                    logging.AddEventSourceLogger();
                }
            })
            .ConfigureAppConfiguration((hostingContext, config) =>
            {
                var env = hostingContext.HostingEnvironment;
                Console.WriteLine($"{env}");
                config.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true, reloadOnChange: true)
                .AddJsonFile(Path.Combine("configuration", $"hostsettings.json"), optional: true, reloadOnChange: true)
                .AddJsonFile(Path.Combine("configuration", $"configuration.json"), optional: true, reloadOnChange: true)
                .AddJsonFile(Path.Combine("configuration", $"configuration.{env.EnvironmentName}.json"), optional: true, reloadOnChange: true);

                config.AddEnvironmentVariables();
            })
            .UseStartup<Startup>();
    }
}
