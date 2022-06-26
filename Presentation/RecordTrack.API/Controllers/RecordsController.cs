using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecordTrack.Application.Abstractions;
using RecordTrack.Application.Repositories;
using RecordTrack.Application.ViewModels.Records;
using RecordTrack.Domain.Entities;

namespace RecordTrack.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RecordsController : ControllerBase
    {
        private readonly IRecordReadRepository _recordReadRepository;
        private readonly IRecordWriteRepository _recordWriteRepository;

        public RecordsController(IRecordReadRepository recordReadRepository, IRecordWriteRepository recordWriteRepository)
        {
            _recordReadRepository = recordReadRepository;
            _recordWriteRepository = recordWriteRepository;

        }

        //[HttpGet]
        //public async Task Get()
        //{
        //    Record r = await _recordReadRepository.GetByIdAsync(id, false);

        //}

        [HttpGet]
        public IActionResult Get()
        {
            return Ok(_recordReadRepository.GetAll(false));
        }

        [HttpPost]
        public async Task<IActionResult> Create(VM_Create_Record model)
        {
            if(ModelState.IsValid)
            _recordWriteRepository.AddAsync(new()
            {
                Name = model.Name,
                Price = model.Price,
                Stock = model.Stock
            });
            await _recordWriteRepository.SaveAsync();
            return Ok();
        }
        [HttpPut]
        public async Task<IActionResult> Update(VM_Update_Record model, string id)
        {
            Record record = await _recordReadRepository.GetByIdAsync(id);
            record.Stock = model.Stock;
            record.Name = model.Name;
            record.Price = model.Price;
            await _recordWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(string id)
        {
            await _recordWriteRepository.Remove(id);
            _recordWriteRepository.SaveAsync();
            return Ok();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(string id)
        {
            return Ok(await _recordReadRepository.GetByIdAsync(id, false));
        }
    }
}
