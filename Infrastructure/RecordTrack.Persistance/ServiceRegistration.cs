using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using RecordTrack.Application.Abstractions;
using RecordTrack.Persistance.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordTrack.Persistance.Contexts;
using Microsoft.Extensions.Configuration;
using RecordTrack.Persistence;
using RecordTrack.Application.Repositories;

namespace RecordTrack.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            
            services.AddDbContext<RecordTrackDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));
            services.AddSingleton<IRecordService, RecordService>();

            services.AddScoped<ICustomerReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IRecordReadRepository, RecordReadRepository>();
            services.AddScoped<IRecordWriteRepository, RecordWriteRepository>();
        }
    }
}
