using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shukhlyada.Api.DTOs.Reports
{
    public class CreateReportDTO
    {
        public Guid PostId { get; set; }
        public string Reason { get; set; }
        public int Type { get; set; }

    }
    
}

