using Microsoft.EntityFrameworkCore;
using System_Zarzadzania_Lotami.Data;
using System_Zarzadzania_Lotami.Services;

var builder = WebApplication.CreateBuilder(args);

//Database
builder.Services.AddDbContext<FlightSystemContext>(
    options =>
        options.UseInMemoryDatabase("InMemoryDbForTesting"), ServiceLifetime.Transient);


// Add services to the container.

builder.Services.AddScoped<IFlightService, FlightService>();

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


//Create scope for dbcontext
using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<FlightSystemContext>();
    dbContext.Database.EnsureCreated();
}


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
