using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class UpdateFeedbackAppointmentRequest
    {
        public double? FeedbackRating { get; set; }
        public string? FeedbackContent { get; set; }
        public DateTime? FeedbackDate { get; set; }
    }
}
