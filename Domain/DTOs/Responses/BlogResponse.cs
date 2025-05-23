﻿namespace Domain.DTOs.Responses
{
    public class BlogResponse
    {
        public int BlogId { get; set; }
        public string ImageUrl { get; set; }
        public string Title { get; set; }
        public string? Description { get; set; }
        public string Content { get; set; }
        public int Status { get; set; }
        public int? TherapistId { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
