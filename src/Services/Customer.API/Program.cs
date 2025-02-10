using Common.Logging;
using Customer.API.Configurations;
using Customer.API.Extensions;
using Serilog;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
var config = builder.Configuration;
builder.Host.UseSerilog(Serilogger.Configure);

Log.Information("Start Customer API up");

try
{
    // Add services to the container.
    builder.Services.AddCoreDependencies(config);
    builder.Services.AddAutoMapper(typeof(Program).Assembly);

    builder.Services.AddControllers();
    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.RunMigration();

    app.Run();
}
catch (Exception ex)
{
    string type = ex.GetType().Name;
    if (type.Equals("HostAbortedException", StringComparison.Ordinal))
    {
        throw;
    }

    Log.Fatal(ex, "Unhandled exception");
}
finally
{
    Log.Information($"Shut down {builder.Environment.ApplicationName} complete");
    Log.CloseAndFlush();
}




