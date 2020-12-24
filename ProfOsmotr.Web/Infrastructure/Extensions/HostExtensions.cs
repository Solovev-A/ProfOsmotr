using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using ProfOsmotr.Hashing;
using ProfOsmotr.DAL;
using ProfOsmotr.DAL.Abstractions;
using System;
using System.Threading.Tasks;

namespace ProfOsmotr.Web.Infrastructure
{
    public static class HostExtensions
    {
        public async static Task<IHost> Seed(this IHost host)
        {
            using (var scope = host.Services.CreateScope())
            {
                var serviceProvider = scope.ServiceProvider;

                var uow = serviceProvider.GetRequiredService<IProfUnitOfWork>();
                var passwordHasher = serviceProvider.GetRequiredService<IPasswordHasher>();
                var orderDataConfig = serviceProvider.GetRequiredService<OrderDataConfiguration>();
                var dataSeeder = serviceProvider.GetRequiredService<DataSeeder>();
                var logger = serviceProvider.GetRequiredService<ILogger<Program>>();

                if (!await uow.Clinics.AnyAsync() && 
                    !await uow.OrderItems.AnyAsync() && 
                    !await uow.Users.AnyAsync())
                {
                    try
                    {
                        await dataSeeder.Seed(uow, passwordHasher, orderDataConfig);
                    }
                    catch (Exception ex)
                    {
                        logger.LogError(ex, "Во время заполнения БД возникла ошибка");
                    }
                }
            }
            return host;
        }
    }
}