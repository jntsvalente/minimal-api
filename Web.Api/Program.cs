using Web.Api.Endpoints;
using Web.Api.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

builder.Services.AddSingleton(serviceProvider =>
{
    var configuration = serviceProvider.GetRequiredService<IConfiguration>();

    var connectionString = configuration.GetConnectionString("DefaultConnection") ??
    throw new ApplicationException("The connection string is null");

    return new SqlConnectionFactory(connectionString);
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.MapCustomerEndpoints();

app.Run();