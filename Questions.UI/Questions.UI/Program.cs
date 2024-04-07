using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using SautinSoft.Document;
using System.Configuration;
using Infrastructure;
using Aplication.Services;
using Infrastructure.Services;
using Domain.Models;
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IQuestionServiceses, IQuestionService>();
builder.Services.AddDbContext<QuestionDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("ConnetionString")));



// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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
