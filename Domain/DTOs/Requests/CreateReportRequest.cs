using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class CreateReportRequest
    {
        public int? AccountId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
    }
}
