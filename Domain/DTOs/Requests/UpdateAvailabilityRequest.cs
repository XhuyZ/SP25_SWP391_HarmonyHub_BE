using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class UpdateAvailabilityRequest
    {
        public int DayOfWeek { get; set; }
        public TimeOnly FromTime { get; set; }
        public TimeOnly ToTime { get; set; }
        public int TherapistId { get; set; }
    }
}
