﻿namespace Domain.DTOs.Responses;

public class QuizResponse
{
    public int Id { get; set; }
    public string ImageUrl { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public int Status { get; set; }
    public int TherapistId { get; set; }
    
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public List<QuestionResponse> QuestionResponse { get; set; } = new List<QuestionResponse>();
    public List<ResultResponse> ResultResponse { get; set; } = new List<ResultResponse>();
}