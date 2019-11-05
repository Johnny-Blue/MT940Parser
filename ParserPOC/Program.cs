namespace ParserPOC
{
    using AutoMapper;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using Microsoft.Extensions.Logging;
    using MT940Data.Entities;
    using ParserPOC.Services;
    using programmersdigest.MT940Parser.Parsing;
    using programmersdigest.MT940Parser.Store;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var Configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(AppDomain.CurrentDomain.BaseDirectory + "\\appsettings.json", optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();
            var builder = new HostBuilder()
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddEntityFrameworkSqlServer();
                    services.AddDbContextPool<AbnAmroNL>(options => options
                        .UseSqlServer(Configuration.GetConnectionString("AbnAmroNL")))
                    .AddLogging(logbuilder =>
                    {
                        logbuilder.ClearProviders().AddConsole();
                    })
                    .AddScoped<IParser, Parser>()
                    .AddScoped<IStoreMT940, StoreMT940>()
                    .AddScoped<IMT940Service, MT940Service>().Configure<MT940ServiceConfig>(Configuration.GetSection("MT940Service"))
                    .AddAutoMapper(typeof(MT940Data.AutoMapperProfile));
                })
                .UseConsoleLifetime()
                .Build();
            var service = builder.Services.GetService<IMT940Service>();
            await service.ExecuteAsync().ConfigureAwait(false);
        }
    }
}
