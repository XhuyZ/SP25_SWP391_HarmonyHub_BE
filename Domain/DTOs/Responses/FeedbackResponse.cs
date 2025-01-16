using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.DTOs.Responses
{
    public class FeedbackResponse
    {
        public double Rating { get; set; }
        public string Content { get; set; }
        public int? MemberId { get; set; }
        public Account? Member { get; set; }
        public int? PackageId { get; set; }
        public Package Package { get; set; }
    }
}
