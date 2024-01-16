using Microsoft.EntityFrameworkCore;
using MillionAndUp.Data;
using MillionAndUp.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddScoped<IPropertyService, PropertyService>();

IConfiguration configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json")
            .Build();

string connectionString = configuration.GetConnectionString("DefaultConnection");

//DTRUJILLO - Add DbContext 
builder.Services.AddDbContext<PropertyDbContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AllowEveryone", policy =>
        policy.RequireAssertion(context => true));
});
builder.Services.AddScoped<PropertyService>();

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

app.Run();
