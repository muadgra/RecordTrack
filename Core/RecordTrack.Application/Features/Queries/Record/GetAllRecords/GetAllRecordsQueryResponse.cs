using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application.Features.Queries.Record.GetAllRecords
{
    public class GetAllRecordsQueryResponse
    {
        public int TotalCount { get; set; }
        public object Records { get; set;}

        public GetAllRecordsQueryResponse () { }
    }
}
