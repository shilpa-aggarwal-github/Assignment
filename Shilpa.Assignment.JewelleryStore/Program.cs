using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using Shilpa.Assignment.Database.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shilpa.Assignment.JewelleryStore
{
    public class Program
    {
        [Obsolete]
        public static async Task Main(string[] args)
        {
           var host= CreateHostBuilder(args).Build();
            await SeedJewelleryStroreData(host);
            try
            {
                host.Run();
            }
            catch(Exception ex)
            {
                Log.Fatal(ex, "host terminated unexpectedly");
            }
            finally
            {
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });

        [Obsolete]
        private static async Task SeedJewelleryStroreData(IHost host)
        {
            using(var scope = host.Services.CreateScope())
            {
                var services = scope.ServiceProvider;
                try
                {
                    var context = services.GetRequiredService<JewelleryContext>();
                    await context.Database.MigrateAsync();

                    var env = services.GetRequiredService<Microsoft.AspNetCore.Hosting.IHostingEnvironment>();
                    await Database.Seed.DBInitializer.InitializeAsync(context, env);
                }
                catch(Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError("0000", ex, "An Error occurred while seeding the database");
                }
            }
        }
    }
}
