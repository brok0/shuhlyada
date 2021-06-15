using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Shukhlyada.Api.DTOs.Reports
{
    public class ReadReportDTO
    {
        public Guid PostId { get; set; }
        public string Reason { get; set; }

        public ReportType Type { get; set; }
    }
    public enum ReportType
    {
        Racism,
        Offensive,
        Pornography,
        //...
    }
}
