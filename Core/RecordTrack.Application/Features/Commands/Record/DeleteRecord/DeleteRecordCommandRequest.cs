using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application.Features.Commands.Record.DeleteRecord
{
    public class DeleteRecordCommandRequest : IRequest<DeleteRecordCommandResponse>
    {
        public string Id { get; set; }
    }
}
