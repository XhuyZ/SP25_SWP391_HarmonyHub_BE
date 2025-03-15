using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTOs.Requests
{
    public class UpdateBlogRequest
    {
        public string Title { get; set; }
        public string Content { get; set; }
    }
}
