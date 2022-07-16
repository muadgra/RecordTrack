using RecordTrack.Application.Repositories.RecordImageFile;
using RecordTrack.Persistance.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Persistance.Repositories.RecordImageFile
{
    public class RecordImageFileReadRepository : ReadRepository<Domain.Entities.RecordImageFile>, IRecordImageFileReadRepository
    {
        public RecordImageFileReadRepository(RecordTrackDbContext context) : base(context)
        {
        }
    }
}
