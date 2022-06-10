using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecordTrack.Application.Abstractions;

namespace RecordTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordService _recordService;

        public RecordsController(IRecordService recordService)
        {
            _recordService = recordService;
        }

        [HttpGet]
        public IActionResult GetRecords()
        {
            var records = _recordService.GetAllRecords();
            return Ok(records);
        }
    }
}
