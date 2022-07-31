using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application.Features.Queries.RecordImageFile
{
    public class GetRecordImagesQueryRequest: IRequest<List<GetRecordImagesQueryResponse>>
    {
        public string Id { get; set; }
    }
}
