using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Entities;

namespace Domain.DTOs.Requests
{
    public class CreateFeedbackRequest
    {
        public double Rating { get; set; }
        public string Content { get; set; }
        public int? MemberId { get; set; }
        public int? PackageId { get; set; }
    }
}
