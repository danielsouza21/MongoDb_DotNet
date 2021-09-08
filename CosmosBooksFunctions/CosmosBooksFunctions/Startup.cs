using CosmosBooksFunctions;
using CosmosBooksFunctions.Services;
using Microsoft.Azure.Functions.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Driver;
using System.IO;
using System.Security.Authentication;

[assembly: FunctionsStartup(typeof(Startup))]
namespace CosmosBooksFunctions
{
    public class Startup : FunctionsStartup
    {
        public override void Configure(IFunctionsHostBuilder builder)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile(Constants.LOCAL_SETTINGS_FILE, optional: true, reloadOnChange: true)
                .AddEnvironmentVariables()
                .Build();

            builder.Services.AddSingleton<IConfiguration>(config);

            var mongoClient = GetMongoDbClient(config);
            builder.Services.AddSingleton(mongoClient);

            builder.Services.AddTransient<IBookService, BookService>();
        }

        private static MongoClient GetMongoDbClient(IConfigurationRoot config)
        {
            var settings = MongoClientSettings.FromUrl(new MongoUrl(config[Constants.CONNECTION_STRING_CONFIG_INDEX]));
            settings.SslSettings = new SslSettings() { EnabledSslProtocols = SslProtocols.Tls12 };
            return new MongoClient(settings);
        }
    }
}