using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordTrack.Application.Abstractions;
using RecordTrack.Domain.Entities;

namespace RecordTrack.Persistance.Concretes
{
    public class RecordService : IRecordService
    {
        public List<Record> GetAllRecords()
            => new()
            {
                new() { Id = Guid.NewGuid(), Name = "Birinci", Stock = 1, Price = 100 },
                new() { Id = Guid.NewGuid(), Name = "İkinci", Stock = 2, Price = 250 },
                new() { Id = Guid.NewGuid(), Name = "Üçüncü", Stock = 3, Price = 500 },
            };
    }
}
