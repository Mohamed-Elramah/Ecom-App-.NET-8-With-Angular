using Ecom.Core.interfaces;
using Ecom.Core.Services;
using Ecom.infrastructure.Data;
using Ecom.infrastructure.Repositries;
using Ecom.infrastructure.Repositries.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure
{
    public static class infrastructureRegisteration
    {
        public static IServiceCollection infrastructureConfiguration(this IServiceCollection services , IConfiguration configuration)
        {
            // apply unit of work 
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IImageManagmentService, ImageManagmentService>();
            services.AddSingleton<IFileProvider>(
                new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(),"wwwroot")));

            // apply DbContext
            services.AddDbContext<AppDbContext>(op=>
            {
                op.UseSqlServer(configuration.GetConnectionString("EcomDatabase"));
            });
            return services;
        }
    }
}
