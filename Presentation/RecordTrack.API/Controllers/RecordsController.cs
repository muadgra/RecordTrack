using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecordTrack.Application.Abstractions;
using RecordTrack.Application.Abstractions.Storage;
using RecordTrack.Application.Features.Commands.Record.CreateRecord;
using RecordTrack.Application.Features.Commands.Record.DeleteRecord;
using RecordTrack.Application.Features.Commands.Record.UpdateRecord;
using RecordTrack.Application.Features.Commands.RecordImageFile.RemoveRecordImage;
using RecordTrack.Application.Features.Commands.RecordImageFile.UploadRecordImage;
using RecordTrack.Application.Features.Queries.Record.GetAllRecords;
using RecordTrack.Application.Features.Queries.Record.GetRecordById;
using RecordTrack.Application.Features.Queries.RecordImageFile;
using RecordTrack.Application.Repositories;
using RecordTrack.Application.Repositories.File;
using RecordTrack.Application.Repositories.InvoiceFile;
using RecordTrack.Application.Repositories.RecordImageFile;
using RecordTrack.Application.RequestParameters;
using RecordTrack.Application.ViewModels.Records;
using RecordTrack.Domain.Entities;

namespace RecordTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public RecordsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] GetAllRecordsQueryRequest getAllRecordsQueryRequest)
        {
            GetAllRecordsQueryResponse response = await _mediator.Send(getAllRecordsQueryRequest);
            return Ok(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateRecordCommandRequest createRecordCommandRequest)
        {
            CreateRecordCommandResponse response = await _mediator.Send(createRecordCommandRequest);

            return Ok(response);
        }
        [HttpPut]
        public async Task<IActionResult> Update(UpdateRecordCommandRequest request)
        {
            return Ok(await _mediator.Send(request));
        }

        [HttpDelete("{Id}")]
        public async Task<IActionResult> Delete([FromRoute] DeleteRecordCommandRequest request)
        {
            
            return Ok(await _mediator.Send(request));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get([FromQuery] GetRecordByIdQueryRequest getRecordByIdRequest)
        {
            return Ok(await _mediator.Send(getRecordByIdRequest));
        }

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload(UploadRecordImageCommandRequest request)
        {
            await _mediator.Send(request);
            return Ok();
        }

        [HttpGet("[action]/{id}")]
        public async Task<IActionResult> GetRecordImages([FromRoute] GetRecordImagesQueryRequest request)
        {
            return Ok(_mediator.Send(request));
        }

        [HttpDelete("[action]/{recordId}/imageId")]
        public async Task<IActionResult> DeleteRecordImage([FromRoute] RemoveRecordImageCommandRequest request, [FromQuery] string imageId)
        {
            request.ImageId = imageId;
            return Ok(_mediator.Send(request));
        }
    }
}
