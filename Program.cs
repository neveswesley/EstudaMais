using System.Text.Json.Serialization;
using ConsultasEF.Db;
using ConsultasEF.Db.Repositories;
using ConsultasEF.Db.Repositories.Interfaces;
using ConsultasEF.Services;
using ConsultasEF.Services.Interfaces;
using ConsultasEF.Validations;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
using SeuProjeto.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ConsultasContext>(options =>
    options.UseSqlServer(
        "Server=WESLEY\\SQLEXPRESS;Database=Consultas.Db;User ID=sa;Password=1q2w3e4r5t@#;TrustServerCertificate=True;"));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<IValidator<Aluno>, AlunoValidation>();
builder.Services.AddScoped<IAlunoRepository, AlunoRepository>();
builder.Services.AddScoped<IAlunoService, AlunoService>();
builder.Services.AddScoped<ICursoRepository, CursoRepository>();
builder.Services.AddScoped<ICursoService, CursoService>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

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