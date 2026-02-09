using Microsoft.EntityFrameworkCore;
using MiniProjectExam1.Entities;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Configure SQL Server
builder.Services.AddEntityFrameworkSqlServer();
builder.Services.AddDbContextPool<MiniProjectExam1Context>(options =>
{
    var conString = configuration.GetConnectionString("SqlServerDB");
    options.UseSqlServer(conString);
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseAuthorization();

app.MapControllers();

app.Run();
