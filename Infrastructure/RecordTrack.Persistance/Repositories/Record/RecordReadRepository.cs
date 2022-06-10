using RecordTrack.Application.Abstractions;
using RecordTrack.Domain.Entities;
using RecordTrack.Persistance.Contexts;
using RecordTrack.Persistance.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application.Repositories
{
    public class RecordReadRepository : ReadRepository<Record>, IRecordReadRepository
    {
        public RecordReadRepository(RecordTrackDbContext context) : base(context)
        {
        }
    }
}
