using FastEndpoints;
using FastEndpoints.Swagger;
using Routes.API.Configuration;
using Routes.Application;
using Routes.Application.Commands.UpdateRoute;
using Routes.Application.Common.Result;
using Routes.Application.Queries.GetRoutes;
using Routes.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.UseConfiguration(builder.Configuration);

var app = builder.Build();

app.UseConfiguration();

app.Run();