using Pago.API.Configurations;
using Steeltoe.Extensions.Configuration.ConfigServer;
using Pago.Application;
using Pago.Infrastructure;
using Pago.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddConfigServer(
    LoggerFactory.Create(builder =>
    {
        builder.AddConsole();
    })
    );
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//Capa de aplicacion
builder.Services.AddApplication();
//Capa de infra
var connectionString = builder.Configuration["dbPago-cnx"];
builder.Services.AddInfraestructure(builder.Configuration);
builder.Services.AddHealthCheckConfiguration(builder.Configuration);
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.UseHealthCheckConfiguration();

app.UseCustomExceptionHandler();

app.MapControllers();

app.Run();
