using MediatR;
using RecordTrack.Application.RequestParameters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application.Features.Queries.Record.GetAllRecords
{
    public class GetAllRecordsQueryRequest : IRequest<GetAllRecordsQueryResponse>
    {
        // public Pagination Pagination { get; set; }
        public int Page { get; set; } = 0;
        public int Size { get; set; } = 5;

    }
}
