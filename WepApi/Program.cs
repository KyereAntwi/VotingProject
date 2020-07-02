using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DTOs.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace WepApi
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var host = CreateHostBuilder(args).Build();

            using (var serviceScope = host.Services.CreateScope()) 
            {
                var dbContext = serviceScope.ServiceProvider.GetRequiredService<DTOs.Data.DbContext>();
                await dbContext.Database.MigrateAsync();

                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync("Administrator")) 
                {
                    var adminRole = new IdentityRole("Administrator");
                    await roleManager.CreateAsync(adminRole);
                }

                if (!await roleManager.RoleExistsAsync("EC-Official"))
                {
                    var adminRole = new IdentityRole("EC-Official");
                    await roleManager.CreateAsync(adminRole);
                }

                if (!await roleManager.RoleExistsAsync("Voter"))
                {
                    var adminRole = new IdentityRole("Voter");
                    await roleManager.CreateAsync(adminRole);
                }
            }

                await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
