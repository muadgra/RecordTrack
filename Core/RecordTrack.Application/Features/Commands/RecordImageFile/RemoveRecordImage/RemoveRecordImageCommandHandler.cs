using MediatR;
using Microsoft.EntityFrameworkCore;
using RecordTrack.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application.Features.Commands.RecordImageFile.RemoveRecordImage
{
    public class RemoveRecordImageCommandHandler : IRequestHandler<RemoveRecordImageCommandRequest, RemoveRecordImageCommandResponse>
    {
        readonly IRecordReadRepository _recordReadRepository;
        readonly IRecordWriteRepository _recordWriteRepository;

        public RemoveRecordImageCommandHandler(IRecordReadRepository recordReadRepository, IRecordWriteRepository recordWriteRepository)
        {
            _recordReadRepository = recordReadRepository;
            _recordWriteRepository = recordWriteRepository;
        }

        public async Task<RemoveRecordImageCommandResponse> Handle(RemoveRecordImageCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Record record = await _recordReadRepository.Table.Include(r => r.RecordImageFiles).FirstOrDefaultAsync(r => r.Id == Guid.Parse(request.Id));
            Domain.Entities.RecordImageFile recordImageFile = record?.RecordImageFiles.FirstOrDefault(r => r.Id == Guid.Parse(request.ImageId));

            if(recordImageFile != null)
            {
                record.RecordImageFiles.Remove(recordImageFile);
            }
            await _recordWriteRepository.SaveAsync();
            return new();
        }
    }
}
