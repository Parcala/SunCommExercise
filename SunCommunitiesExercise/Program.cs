using SunCommunitiesExercise.Models;
using SunCommunitiesExercise.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddScoped<IFeeCalculator, FeeCalculator>();
builder.Services.Configure<FeeOptions>(builder.Configuration.GetSection(nameof(FeeOptions)));

builder.Services.AddLogging();

// Endpoints and Swagger
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

app.Run();
