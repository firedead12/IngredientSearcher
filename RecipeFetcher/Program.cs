using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using IngredientSearcher.DataAccess;
using IngredientSearcher.RecipeFetcher.Fetchers.InitialFetcher;
using IngredientSearcher.RecipeFetcher.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace IngredientSearcher.RecipeFetcher
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureAppConfiguration(((context, builder) =>
                {
                    builder.AddJsonFile("appsettings.json");
                    if (context.HostingEnvironment.IsDevelopment())
                        builder.AddUserSecrets<Program>();
                }))
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddDbContext<RecipeContext>(options =>
                        options.UseNpgsql(hostContext.Configuration.GetConnectionString("RecipeContext"), b =>
                        {
                            b.ProvideClientCertificatesCallback(certificates =>
                            {
                                certificates.Add(new X509Certificate2(hostContext.Configuration.GetValue<string>("DBCertificate")));
                            });
                        }));
                    services.AddTransient<InitialFetcher>();
                    services.AddTransient<ServiceResolver>(provider => key =>
                    {
                        return key switch
                        {
                            _ => provider.GetService<InitialFetcher>()
                        };
                    });
                    services.AddHostedService<Worker>();
                });
    }
    
    public delegate IRecipeFetcher ServiceResolver(string key);
}