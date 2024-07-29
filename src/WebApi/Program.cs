using System.Text.Json.Serialization;
using WebApi.Extensions;
using WebApi.Filters;
using WebApi.Middlewares;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(options =>
{
	options.Filters.Add<NotificationFilter>();
}).AddJsonOptions(options => options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddUseCase();
builder.Services.AddControllerLayerDI();
builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddIntegration();


var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseSwagger();
app.UseSwaggerUI();


app.MigrationInitialisation();

app.UseHttpsRedirection();

app.UseMiddleware(typeof(ErrorMiddleware));

app.UseAuthorization();

app.MapControllers();

app.Run();