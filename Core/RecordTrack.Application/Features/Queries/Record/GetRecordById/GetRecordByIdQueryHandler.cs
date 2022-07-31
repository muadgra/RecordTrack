using MediatR;
using RecordTrack.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application.Features.Queries.Record.GetRecordById
{
    public class GetRecordByIdQueryHandler : IRequestHandler<GetRecordByIdQueryRequest, GetRecordByIdQueryResponse>
    {
        readonly IRecordImageReadRepository _recordReadRepository;

        public GetRecordByIdQueryHandler(IRecordImageReadRepository recordReadRepository)
        {
            _recordReadRepository = recordReadRepository;
        }

        public async Task<GetRecordByIdQueryResponse> Handle(GetRecordByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var record = await _recordReadRepository.GetByIdAsync(request.Id, false);
            return new()
            {
                Price = record.Price,
                Name = record.Name,
                Stock = record.Stock
            };

        }
    }
}
