using MerlAIn.Agents;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddHostedService<AgentRunWorker>();

IHost host = builder.Build();

host.Run();
