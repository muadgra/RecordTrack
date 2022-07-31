﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RecordTrack.Domain.Entities
{
    public class RecordImageFile : File
    {
        public ICollection<Record> Records { get; set; }
    }
}
