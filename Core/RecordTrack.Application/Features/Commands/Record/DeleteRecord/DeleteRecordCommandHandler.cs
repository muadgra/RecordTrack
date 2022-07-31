using MediatR;
using RecordTrack.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application.Features.Commands.Record.DeleteRecord
{
    public class DeleteRecordCommandHandler : IRequestHandler<DeleteRecordCommandRequest, DeleteRecordCommandResponse>
    {
        readonly IRecordWriteRepository _recordWriteRepository;
        public async Task<DeleteRecordCommandResponse> Handle(DeleteRecordCommandRequest request, CancellationToken cancellationToken)
        {
            await _recordWriteRepository.Remove(request.Id);
            await _recordWriteRepository.SaveAsync();
            return new();
        }
    }
}
