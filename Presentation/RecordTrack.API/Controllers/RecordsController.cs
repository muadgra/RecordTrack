using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RecordTrack.Application.Abstractions;
using RecordTrack.Application.Abstractions.Storage;
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
        private readonly IRecordReadRepository _recordReadRepository;
        private readonly IRecordWriteRepository _recordWriteRepository;
        private IWebHostEnvironment _webHostEnvironment;
        private readonly IFileWriteRepository _fileWriteRepository;
        private readonly IFileReadRepository _fileReadRepository;
        private readonly IRecordImageFileReadRepository _recordImageReadRepository;
        private readonly IRecordImageFileWriteRepository _recordImageWriteRepository;
        private readonly IInvoiceFileReadRepository _invoiceFileReadRepository;
        private readonly IInvoiceFileWriteRepository _invoiceFileWriteRepository;
        private readonly IStorageService _storageService;
        public RecordsController(IRecordReadRepository recordReadRepository, 
            IRecordWriteRepository recordWriteRepository, 
            IWebHostEnvironment webHostEnvironment, 
            IFileWriteRepository fileWriteRepository, 
            IFileReadRepository fileReadRepository, 
            IRecordImageFileReadRepository recordImageReadRepository,
            IRecordImageFileWriteRepository recordImageWriteRepository,
            IInvoiceFileReadRepository invoiceFileReadRepository, 
            IInvoiceFileWriteRepository invoiceFileWriteRepository,
            IStorageService storageService)
        {
            _recordReadRepository = recordReadRepository;
            _recordWriteRepository = recordWriteRepository;
            _webHostEnvironment = webHostEnvironment;
            _fileWriteRepository = fileWriteRepository;
            _fileReadRepository = fileReadRepository;
            _recordImageReadRepository = recordImageReadRepository;
            _recordImageWriteRepository = recordImageWriteRepository;
            _invoiceFileReadRepository = invoiceFileReadRepository;
            _invoiceFileWriteRepository = invoiceFileWriteRepository;
            _storageService = storageService;
        }

        //[HttpGet]
        //public async Task Get()
        //{
        //    Record r = await _recordReadRepository.GetByIdAsync(id, false);

        //}

        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] Pagination pagination)
        {
            var totalCount = _recordReadRepository.GetAll(false).Count();
            var records = _recordReadRepository.GetAll(false).Select(r => new
            {
                r.Id,
                r.Name,
                r.UpdateDate,
                r.CreateDate,
                r.Price,
                r.Stock
            }).Skip(pagination.Page * pagination.Size).Take(pagination.Size);
            return Ok(new
            {
                totalCount,
                records
            });
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

        [HttpPost("[action]")]
        public async Task<IActionResult> Upload()
        {
           var data = await _storageService.UploadAsync("resource/files", Request.Form.Files);
            await _recordImageWriteRepository.AddRangeAsync(data.Select(d => new RecordImageFile()
            {
                FileName = d.fileName,
                FilePath = d.pathOrContainerName,
                StorageType = _storageService.StorageName
            }).ToList());
            
            return Ok();
        }


    }
}
