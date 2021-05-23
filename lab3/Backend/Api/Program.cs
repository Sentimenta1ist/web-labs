using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Server.HttpSys;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Elfo.Round.Mvc;
using Microsoft.Extensions.Hosting;
using Autofac.Extensions.DependencyInjection;

namespace Elfo.POC.PdfGallery.Api
{
    public class Program
    {
        private static IConfigurationRoot configuration;

        public static async Task<int> Main(string[] args)
        {
            try
            {
                Serilog.Debugging.SelfLog.Enable(msg => Debug.WriteLine(msg));

                configuration = new ConfigurationBuilder()
                                            .SetBasePath(Directory.GetCurrentDirectory())
                                            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                                            .Build();

                Log.Logger = new LoggerConfiguration()
                    .ReadFrom.Configuration(configuration)
                    .Enrich.WithProperty("Application", configuration["ApiName"])
                    .CreateLogger();

                Log.ForContext<Program>().Information("Starting web host...");
                var host = BuildHost(args);
                await host.ExecuteTasksBeforeStartup();
                await host.RunAsync();

                return 0;
            }
            catch (Exception ex)
            {
                Log.ForContext<Program>().Fatal(ex, "Host terminated unexpectedly!");
                return 1;
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHost BuildHost(string[] args)
        {
            var host = Host.CreateDefaultBuilder(args)
                           .UseServiceProviderFactory(new AutofacServiceProviderFactory())
                           .ConfigureWebHostDefaults(webBuilder =>
                           {
                               webBuilder.UseStartup<Startup>();
                               webBuilder.UseConfiguration(configuration);

                               if (args.Contains("--httpsyswinauth"))
                               {
                                   webBuilder.UseHttpSys(options =>
                                   {
                                       options.Authentication.Schemes =
                                           AuthenticationSchemes.NTLM | AuthenticationSchemes.Negotiate;
                                       options.Authentication.AllowAnonymous = true;
                                   });
                               }
                           })
                           .UseSerilog();

            return host.Build();
        }
    }
}
