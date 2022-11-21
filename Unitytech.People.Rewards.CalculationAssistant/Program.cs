using Microsoft.EntityFrameworkCore;
using Unitytech.People.Rewards.CalculationAssistant;
using Unitytech.People.Rewards.Data.Repository;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        var connectionString = "Server=unitystationpro.local;Database=RewardDB;User Id=SA;Password=Mbp266320gb-;";
        services.AddDbContext<DatabaseContext>((options) =>
        {
            options.UseSqlServer(connectionString, p => p.MigrationsAssembly("Unitytech.People.Rewards.Server"));
        });
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();

