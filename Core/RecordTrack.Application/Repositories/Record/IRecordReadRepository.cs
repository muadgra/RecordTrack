using RecordTrack.Application.Abstractions;
using RecordTrack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application.Repositories
{
    public interface IRecordReadRepository : IReadRepository<Record>
    {
    }
}
