using RecordTrack.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Application.Abstractions
{
    public interface IRecordService
    {
        List<Record> GetAllRecords();
    }
}
