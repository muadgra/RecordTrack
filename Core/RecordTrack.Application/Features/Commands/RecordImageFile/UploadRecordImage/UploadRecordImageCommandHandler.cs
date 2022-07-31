using MediatR;
using RecordTrack.Application.Abstractions.Storage;
using RecordTrack.Application.Repositories;
using RecordTrack.Application.Repositories.RecordImageFile;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application.Features.Commands.RecordImageFile.UploadRecordImage
{
    public class UploadRecordImageCommandHandler : IRequestHandler<UploadRecordImageCommandRequest, UploadRecordImageCommandResponse>
    {
        readonly IStorageService _storageService;
        readonly IRecordImageFileWriteRepository _recordImageWriteRepository;
        readonly IRecordReadRepository _recordReadRepository;

        public UploadRecordImageCommandHandler(IStorageService storageService, IRecordImageFileWriteRepository recordImageWriteRepository, IRecordReadRepository recordReadRepository)
        {
            _storageService = storageService;
            _recordImageWriteRepository = recordImageWriteRepository;
            _recordReadRepository = recordReadRepository;
        }

        public async Task<UploadRecordImageCommandResponse> Handle(UploadRecordImageCommandRequest request, CancellationToken cancellationToken)
        {
            List<(string fileName, string pathOrContainerName)> result = await _storageService.UploadAsync("record-images", request.Files);
            Domain.Entities.Record record = await _recordReadRepository.GetByIdAsync(request.Id);

            await _recordImageWriteRepository.AddRangeAsync(result.Select(r => new Domain.Entities.RecordImageFile
            {
                FileName = r.fileName,
                FilePath = r.pathOrContainerName,
                StorageType = _storageService.StorageName,
                Records = new List<Domain.Entities.Record>() { record }
            }).ToList());
            await _recordImageWriteRepository.SaveAsync();
            return new();
        }
    }
}
