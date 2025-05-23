﻿using AutoMapper;
using Domain.DTOs.Common;
using Domain.DTOs.Responses;
using Domain.Entities;

namespace Domain.Automapper;

public class ResponseMappingProfile : Profile
{
    public ResponseMappingProfile()
    {
        CreateMap(typeof(PagedList<>), typeof(PagedList<>));

        CreateMap<Account, AccountResponse>().ReverseMap();

        CreateMap<Blog, BlogResponse>()
            .ForMember(dest => dest.BlogId, opt => opt.MapFrom(src => src.Id)).ReverseMap();

        CreateMap<Option, OptionResponse>()
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ReverseMap();

        CreateMap<Result, ResultResponse>();

        CreateMap<Question, QuestionResponse>()
            .ForMember(dest => dest.id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.OptionResponse, opt => opt.MapFrom(src => src.Options.Any()
                ? src.Options.Select(o => new OptionResponse { Content = o.Content }).ToList()
                : new List<OptionResponse>()))
            .ReverseMap();

        CreateMap<Quiz, QuizResponse>()
            .ForMember(dest => dest.QuestionResponse, opt => opt.MapFrom(src => src.QuizQuestions.Select(q =>
                new QuestionResponse
                {
                    id = q.Question.Id,
                    Content = q.Question.Content,
                    OptionResponse = q.Question.Options.Any()
                        ? q.Question.Options.Select(o => new OptionResponse { id = o.Id, Content = o.Content, Type = o.Type }).ToList()
                        : new List<OptionResponse>()
                })))
            .ForMember(dest => dest.ResultResponse, opt => opt.MapFrom(src => src.Results.Select(r => new ResultResponse
            {
                Id = r.Id,
                Type = r.Type,
                Content = r.Content
            })))
            .ReverseMap();

        CreateMap<QuizQuestion, QuizQuestionResponse>()
            .ForMember(dest => dest.QuizId, opt => opt.MapFrom(src => src.QuizId))
            .ForMember(dest => dest.QuestionId, opt => opt.MapFrom(src => src.QuestionId))
            .ReverseMap();


        CreateMap<Account, LoginResponse>()
            .ForMember(dest => dest.AccountId, opt =>
                opt.MapFrom(src => src.Id))
            .ReverseMap();

        CreateMap<Account, TherapistDetailsResponse>().ReverseMap();
        CreateMap<Account, MemberDetailsResponse>().ReverseMap();
        CreateMap<Account, TherapistProfileResponse>().ReverseMap();
        
        CreateMap<Availability, AvailabilityResponse>().ReverseMap();
        
        CreateMap<Qualification, QualificationResponse>().ReverseMap();
        
        CreateMap<Specialty, SpecialtyResponse>().ReverseMap();
        
        CreateMap<Package, PackageResponse>().ReverseMap();

        CreateMap<Appointment, AppointmentResponse>()
            .ForMember(dest => dest.MemberFullName, opt =>
                opt.MapFrom(src => src.Member.FirstName + " " + src.Member.LastName))
            .ForMember(dest => dest.TherapistFullName, opt =>
                opt.MapFrom(src => src.Therapist.FirstName + " " + src.Therapist.LastName))
            .ForMember(dest => dest.PackageName, opt =>
                opt.MapFrom(src => src.Package.Name))
            .ReverseMap();

        CreateMap<Availability, AvailabilityResponse>().ReverseMap();

        CreateMap<Transaction, TransactionResponse>()
            .ForMember(dest => dest.SenderFullName, opt =>
                opt.MapFrom(src => src.Sender.FirstName + " " + src.Sender.LastName))
            .ForMember(dest => dest.ReceiverFullName, opt =>
                opt.MapFrom(src => src.Receiver.FirstName + " " + src.Receiver.LastName))
            .ReverseMap();

        CreateMap<Report, ReportResponse>().ReverseMap();
    }
}