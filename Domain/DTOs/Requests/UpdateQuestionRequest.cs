using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class UpdateQuestionRequest
    {
        public int id { get; set; }
        public string Content { get; set; }

        public List<OptionRequest> Options { get; set; }
    }
}
