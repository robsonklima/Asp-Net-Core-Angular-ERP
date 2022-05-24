using SAT.SERVICES.Interfaces;
using SAT.SERVICES.Services;
using Microsoft.AspNetCore.Hosting;
using SAT.TASKS;
using SAT.INFRA.Context;
using Microsoft.EntityFrameworkCore;

using IHost host = Host.CreateDefaultBuilder(args)
    .UseWindowsService(options =>
    {
        options.ServiceName = "SAT.V2 TASKS";
    })
    .ConfigureServices(services =>
    {
        services.AddDbContext<AppDbContext>(
                options => options.UseSqlServer("Data Source=satdbprod.perto.com.br;Initial Catalog=SAT;User Id=satweb;Password=Nkhq8297b589a2c4;Connection Timeout=3600;",
                sqlServerOptions => sqlServerOptions.CommandTimeout(180)));

        services.AddScoped<IEquipamentoContratoService, EquipamentoContratoService>();
        services.AddTransient<TesteEquip>();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();

