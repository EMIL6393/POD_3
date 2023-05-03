using Microsoft.EntityFrameworkCore;
using POD_3.BLL.Repositories.Impl;
using POD_3.BLL.Repositories.Repository;
using POD_3.BLL.Services.Implementation;
using POD_3.BLL.Services.Interfaces;
using POD_3.Context;
using POD_3.Core;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddJsonOptions(x=>
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(Program).Assembly);
//builder.Services.AddScoped<ISubscriptionPlanRepository, SubscriptionPlanRepository>();
builder.Services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();

builder.Services.AddScoped<ISupportTicketRepository, SupportTicketRepository>();
builder.Services.AddScoped<ITicketSolutionRepository, TicketSolutionRepository>();
builder.Services.AddScoped<ISubscriptionPlanSLARepository, SubscriptionPlanSLARepository>();
builder.Services.AddScoped<ICreateSupportTicketService, CreateSupportTicketService>();
builder.Services.AddScoped<ICloseTicketService, CloseTicketService>();
builder.Services.AddScoped<IFetchAllTicketsByUserNameService, FetchAllTicketsByUserNameService>();
builder.Services.AddScoped<IFetchOpenTicketsService, FetchOpenTicketsService>();
builder.Services.AddScoped<IFetchTicketByIdService, FetchTicketByIdService>();


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
