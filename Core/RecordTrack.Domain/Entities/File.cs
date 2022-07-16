using RecordTrack.Domain.Entities.Common;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Domain.Entities
{
    public class File : BaseEntity
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string StorageType { get; set; }
        [NotMapped]
        public override DateTime UpdateDate { get => base.UpdateDate; set => base.UpdateDate = value; }
    }
}
