using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class UpdateQuizRequest
    {
        public string Title { get; set; }
        public string? Description { get; set; }
        public List<UpdateQuestionRequest> Questions { get; set; }
    }
}
