using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Responses
{
    public class QuestionResponse
    {
        public string Content { get; set; }
        public List<string> Options { get; set; }
    }
}
