using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class UpdateQuizResultRequest
    {
        public int? Id { get; set; }
        public int Type { get; set; }
        public string Content { get; set; }
    }
}
