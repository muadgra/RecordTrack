using MediatR;
using RecordTrack.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application.Features.Commands.Record.UpdateRecord
{
    public class UpdateRecordCommandHandler : IRequestHandler<UpdateRecordCommandRequest, UpdateRecordCommandReponse>
    {
        readonly IRecordReadRepository _recordReadRepository;
        readonly IRecordWriteRepository _recordWriteRepository;

        public UpdateRecordCommandHandler(IRecordReadRepository recordReadRepository, IRecordWriteRepository recordWriteRepository)
        {
            _recordReadRepository = recordReadRepository;
            _recordWriteRepository = recordWriteRepository;
        }

        public async Task<UpdateRecordCommandReponse> Handle(UpdateRecordCommandRequest request, CancellationToken cancellationToken)
        {
            Domain.Entities.Record record = await _recordReadRepository.GetByIdAsync(request.Id);
            record.Stock = request.Stock;
            record.Price = request.Price;
            record.Name = request.Name;

            await _recordWriteRepository.SaveAsync();
            return new();
        }
    }
}
