using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application.Features.Commands.RecordImageFile.RemoveRecordImage
{
    public class RemoveRecordImageCommandRequest: IRequest<RemoveRecordImageCommandResponse>
    {
        public string Id { get; set; }
        public string? ImageId { get; set; }
    }
}
