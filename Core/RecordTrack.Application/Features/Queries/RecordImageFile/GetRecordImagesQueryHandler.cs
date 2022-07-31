using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RecordTrack.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application.Features.Queries.RecordImageFile
{
    public class GetRecordImagesQueryHandler : IRequestHandler<GetRecordImagesQueryRequest, List<GetRecordImagesQueryResponse>>
    {
        readonly IRecordReadRepository _recordReadRepository;
        readonly IConfiguration configuration;

        public GetRecordImagesQueryHandler(IRecordReadRepository recordReadRepository, IConfiguration configuration)
        {
            _recordReadRepository = recordReadRepository;
            this.configuration = configuration;
        }

        public async Task<List<GetRecordImagesQueryResponse>> Handle(GetRecordImagesQueryRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Record? record = await _recordReadRepository.Table.Include(r => r.RecordImageFiles)
                   .FirstOrDefaultAsync(r => r.Id == Guid.Parse(request.Id));
            return record?.RecordImageFiles.Select(r => new GetRecordImagesQueryResponse
            {
                Path = $"{configuration["BaseStorageUrl"]}/{r.FilePath}",
                FileName = r.FileName,
                Id = r.Id
            }).ToList();
        }
    }
}
