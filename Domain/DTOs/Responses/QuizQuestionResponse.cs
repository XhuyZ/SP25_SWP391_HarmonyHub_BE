using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Responses
{
    public class QuizQuestionResponse
    {
        public int QuizId { get; set; }
        public int QuestionId { get; set; }
    }
}
