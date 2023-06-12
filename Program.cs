using LiteDB;
using URL_Shortener_API.Interfaces;
using URL_Shortener_API.Processor;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IURLShortener, URLShortening>();
builder.Services.AddScoped<IURLRetrieval, URLRetrieval>();
builder.Services.AddSingleton<IKeyGenerator, RandomKeyGenerator>();
builder.Services.AddSingleton<LiteDatabase>(_ => new LiteDatabase("ShortenedLinks.db"));

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
