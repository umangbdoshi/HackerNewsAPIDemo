using HackerNewsAPIDemo.Client;
using HackerNewsAPIDemo.Middlewares;
using HackerNewsAPIDemo.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddMemoryCache();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

builder.Services.AddHttpClient<IHackerNewsClient, HackerNewsClient>();
builder.Services.AddScoped<IHackerNewsStoryService, HackerNewsStoryService>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseMiddleware<ExceptionHandlingMiddleware>();

app.MapControllers();

app.Run();
