using Microsoft.AspNetCore.Hosting;
using MongoDB.Driver;
using System.Net.NetworkInformation;



var builder = WebApplication.CreateBuilder(args);
var MyAllowSpecificOrigins = "CorsPolicy";
// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

foreach (var assembly in AppDomain.CurrentDomain.GetAssemblies())
{
    builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
}

builder.Services.AddMediatR(cfg =>
     cfg.RegisterServicesFromAssembly(typeof(Ping).Assembly));

// Mongo DB Configuration
// Gets the configuration from AppSettings File
IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

string mongoConnectionString = config.GetConnectionString("MongoDB");
var mongoClient = new MongoClient(mongoConnectionString);
var databaseName = "LinkMeApp";
var database = mongoClient.GetDatabase(databaseName);
builder.Services.AddSingleton(database);


//Creates Cors Policy
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
        builder =>
        {
            builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod();
        });
});

var app = builder.Build();

// Configure the HTTP request pipeline.

    app.UseSwagger();
    app.UseSwaggerUI();

app.UseHttpsRedirection();



app.UseCors(MyAllowSpecificOrigins);
app.MapControllers();
app.Run();
