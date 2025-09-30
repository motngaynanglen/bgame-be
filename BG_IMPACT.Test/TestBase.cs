using BG_IMPACT.DTO.Models.Configs.GlobalSetting;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using NUnit.Framework;
using System.Data;

public abstract class TestBase
{
    protected IDbConnection _connection { get; private set; }
    protected ServiceProvider _serviceProvider { get; set; }

    [OneTimeSetUp]
    public void GlobalSetup()
    {

        var config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

        var connString = config.GetConnectionString("TestConnection");

        var services = new ServiceCollection();
        services.DependencyInject(config);
        _serviceProvider = services.BuildServiceProvider();

        AppGlobals.Username = config["Admin:Username"] ?? string.Empty;
        AppGlobals.Password = config["Admin:Password"] ?? string.Empty;
        AppGlobals.ID = Guid.Parse(config["Admin:ID"] ?? string.Empty);

        _connection = new SqlConnection(connString);
        _connection.Open();
    }

    [OneTimeTearDown]
    public void GlobalTeardown()
    {
        _connection?.Dispose();
        _serviceProvider.Dispose();
    }
}
