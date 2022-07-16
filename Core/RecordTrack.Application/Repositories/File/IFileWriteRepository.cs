using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RecordTrack.Application.Abstractions;

namespace RecordTrack.Application.Repositories.File
{
    public interface IFileWriteRepository : IWriteRepository<Domain.Entities.File>
    {
    }
}
