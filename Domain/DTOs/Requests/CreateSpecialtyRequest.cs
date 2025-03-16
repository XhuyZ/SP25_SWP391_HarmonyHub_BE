using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class CreateSpecialtyRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
    }
}
