using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application.Features.Queries.Record.GetRecordById
{
    public class GetRecordByIdQueryRequest: IRequest<GetRecordByIdQueryResponse>
    {
        public string Id { get; set; }
    }
}
