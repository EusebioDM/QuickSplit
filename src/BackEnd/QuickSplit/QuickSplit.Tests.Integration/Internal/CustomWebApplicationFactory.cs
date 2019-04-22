using System;
using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using QuickSplit.Application.Interfaces;
using QuickSplit.Persistence;
using QuickSplit.WebApi;
using QuickSplit.WebApi.Filters;

namespace QuickSplit.Tests.Integration.Internal
{
    public class CustomWebApplicationFactory : WebApplicationFactory<Startup>
    {
        // Pone aca todas las tablas de la base
        private readonly string[] _tables = new[] {"Users"};

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                // Create a new service provider.
                ServiceProvider serviceProvider = new ServiceCollection()
                    .AddEntityFrameworkSqlServer()
                    .AddTransient<IQuickSplitContext, QuickSplitContext>()
                    .AddMediatR(typeof(IQuickSplitContext).Assembly)
                    .BuildServiceProvider();

                // Add a database context (ApplicationDbContext) using an in-memory 
                // database for testing.
                services.AddDbContext<QuickSplitContext>(options =>
                {
                    options.UseSqlServer("Server=localhost;Database=QuickSplitDbTesting;Trusted_Connection=False;User ID=QuickSplit;Password=QuickSplit123;");
                    options.UseInternalServiceProvider(serviceProvider);
                });

                services.AddMvc(AddFilters);

                // Build the service provider.
                ServiceProvider sp = services.BuildServiceProvider();


                // Reseteo la base de datos y le agrego el admin
                using (IServiceScope scope = sp.CreateScope())
                {
                    IServiceProvider scopedServices = scope.ServiceProvider;
                    var db = scopedServices.GetRequiredService<QuickSplitContext>();

                    // Resets Database
                    db.Database.EnsureCreated();
                    foreach (string table in _tables)
                    {
                        db.Database.ExecuteSqlCommand(@"TRUNCATE TABLE dbo." + table);
                    }
                    db.Database.ExecuteSqlCommand(@"INSERT INTO dbo.Users (Name, LastName, Mail, Password) VALUES ('admin', 'admin', 'admin@gmail.com','DVOZUIQnznlVbNpxkYAgwejRW1M=')");
                }
            });
        }

        private void AddFilters(MvcOptions options)
        {
            options.Filters.Add(new ExceptionFilter());
        }
    }
}