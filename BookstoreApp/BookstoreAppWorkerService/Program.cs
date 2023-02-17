using BookstoreAppWorkerService;
using BookstoreAppWorkerService.Data;
using Microsoft.EntityFrameworkCore;

var configuration = new ConfigurationBuilder()
     .AddJsonFile($"appsettings.json");

var config = configuration.Build();
IHost host = Host.CreateDefaultBuilder(args)

    .ConfigureServices(services =>
    {
        //query db
        var connStrQuery = config.GetConnectionString("QueryDb");
        services.AddDbContext<QueryDatabaseContext>(o => o.UseSqlite(connStrQuery));
        //command db
        var connStrCommand = config.GetConnectionString("CommandDb");
        services.AddDbContext<CommandDatabaseContext>(o => o.UseSqlite(connStrCommand));

        services.AddScoped<IBookQuery, DbBookQueryRepo>();
        services.AddScoped<IBookCommand, DbBookCommandRepo>();

        services.AddHostedService<Worker>();

    })
    .Build();

await host.RunAsync();
