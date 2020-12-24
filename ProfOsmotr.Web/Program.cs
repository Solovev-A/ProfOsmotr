using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;
using ProfOsmotr.Web.Infrastructure;
using System.Threading.Tasks;

namespace ProfOsmotr.Web
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            IHost host = await CreateHostBuilder(args)
                .Build()
                .Seed();

            host.Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}