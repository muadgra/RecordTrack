using MediatR;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationServices(this IServiceCollection collection)
        {
            //Assembly'de bulunan tüm sınıfları bul ve ona göre sisteme ekle.
            collection.AddMediatR(typeof(ServiceRegistration));
        }
    }

}
