using RecordTrack.Application.Repositories.RecordImageFile;
using RecordTrack.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Persistance.Repositories.RecordImageFile
{
    public class RecordImageFileWriteRepository : WriteRepository<Domain.Entities.RecordImageFile>, IRecordImageFileWriteRepository
    {
        public RecordImageFileWriteRepository(RecordTrackDbContext context) : base(context)
        {
        }
    }
}
