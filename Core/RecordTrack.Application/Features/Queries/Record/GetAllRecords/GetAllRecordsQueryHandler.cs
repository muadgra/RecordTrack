using MediatR;
using RecordTrack.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application.Features.Queries.Record.GetAllRecords
{
    public class GetAllRecordsQueryHandler : IRequestHandler<GetAllRecordsQueryRequest, GetAllRecordsQueryResponse>
    {
        private readonly IRecordImageReadRepository _recordReadRepository;

        public GetAllRecordsQueryHandler(IRecordImageReadRepository recordReadRepository)
        {
            this._recordReadRepository = recordReadRepository;
        }

        public async Task<GetAllRecordsQueryResponse> Handle(GetAllRecordsQueryRequest request, CancellationToken cancellationToken)
        {
            var totalCount = _recordReadRepository.GetAll(false).Count();
            var records = _recordReadRepository.GetAll(false).Skip(request.Page * request.Size).
                            Take(request.Size).Select(r => new
                            {
                                r.Id,
                                r.Name,
                                r.Stock,
                                r.Price,
                                r.CreateDate,
                                r.UpdateDate
                            }).ToList();
            return new()
            {
                Records = records,
                TotalCount = totalCount
            };
        }
    }
}
