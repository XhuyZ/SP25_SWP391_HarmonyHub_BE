using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Responses
{
    public class QuizResponse
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Status { get; set; }
        public int TherapistId { get; set; }
        public List<Question>QuestionResponse { get; set; }

        //public ICollection<QuizQuestion> QuizQuestions { get; set; }

    }
}
