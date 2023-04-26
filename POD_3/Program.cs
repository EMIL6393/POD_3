using Microsoft.EntityFrameworkCore;
using POD_3.BLL.Repositories.Impl;
using POD_3.BLL.Repositories.Repository;
using POD_3.Context;
using POD_3.Core;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
//builder.Services.AddScoped<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
var constr = builder.Configuration.GetConnectionString("Default");
builder.Services.AddDbContext<DefaultContext>(opt =>
{
    opt.UseSqlServer(constr);
    opt.EnableSensitiveDataLogging();
    opt.EnableServiceProviderCaching();
});
builder.RegisterProjectDependencies();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(opt =>
{
    opt.AllowAnyHeader();
    opt.AllowAnyOrigin();
    opt.AllowAnyMethod();
});

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
