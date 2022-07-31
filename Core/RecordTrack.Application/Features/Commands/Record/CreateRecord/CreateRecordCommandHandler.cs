using MediatR;
using RecordTrack.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application.Features.Commands.Record.CreateRecord
{
    public class CreateRecordCommandHandler : IRequestHandler<CreateRecordCommandRequest, CreateRecordCommandResponse>
    {
        readonly IRecordWriteRepository _recordWriteRepository;

        public CreateRecordCommandHandler(IRecordWriteRepository recordWriteRepository)
        {
            _recordWriteRepository = recordWriteRepository;
        }
        public async Task<CreateRecordCommandResponse> Handle(CreateRecordCommandRequest request, CancellationToken cancellationToken)
        {
                await _recordWriteRepository.AddAsync(new()
                {
                    Name = request.Name,
                    Price = request.Price,
                    Stock = request.Stock
                });
            await _recordWriteRepository.SaveAsync();
            return new();
        }
    }
}
