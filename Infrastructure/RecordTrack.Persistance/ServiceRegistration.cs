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
using RecordTrack.Application.Repositories.File;
using RecordTrack.Persistance.Repositories.File;
using RecordTrack.Persistance.Repositories.RecordImageFile;
using RecordTrack.Application.Repositories.RecordImageFile;
using RecordTrack.Persistance.Repositories.InvoiceFile;
using RecordTrack.Application.Repositories.InvoiceFile;
using RecordTrack.Domain.Entities.Identity;

namespace RecordTrack.Persistance
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceServices(this IServiceCollection services)
        {
            
            services.AddDbContext<RecordTrackDbContext>(options => options.UseSqlServer(Configuration.ConnectionString));

            services.AddIdentity<AppUser, AppRole>(options =>
            {
                options.Password.RequiredLength = 6;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = false;
                options.Password.RequireLowercase = false;
                options.Password.RequireUppercase = false;
                options.User.RequireUniqueEmail = true;
            }).AddEntityFrameworkStores<RecordTrackDbContext>();
            
            
            services.AddSingleton<IRecordService, RecordService>();


            services.AddScoped<IReadRepository, CustomerReadRepository>();
            services.AddScoped<ICustomerWriteRepository, CustomerWriteRepository>();
            services.AddScoped<IOrderReadRepository, OrderReadRepository>();
            services.AddScoped<IOrderWriteRepository, OrderWriteRepository>();
            services.AddScoped<IRecordReadRepository, RecordReadRepository>();
            services.AddScoped<IRecordWriteRepository, RecordWriteRepository>();
            services.AddScoped<IFileWriteRepository, FileWriteRepository>();
            services.AddScoped<IFileReadRepository, FileReadRepository>();
            services.AddScoped<IRecordImageFileWriteRepository, RecordImageFileWriteRepository>();
            services.AddScoped<IRecordImageFileReadRepository, RecordImageFileReadRepository>();
            services.AddScoped<IInvoiceFileReadRepository, InvoiceFileReadRepository>();
            services.AddScoped<IInvoiceFileWriteRepository, InvoiceFileWriteRepository>();

        }
    }
}
