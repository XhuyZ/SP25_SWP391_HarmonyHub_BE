using AutoMapper;
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

        CreateMap<Question, QuestionResponse>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.OptionResponse, opt => opt.MapFrom(src => src.Options.Any()
                ? src.Options.Select(o => new OptionResponse { Content = o.Content }).ToList()
                : new List<OptionResponse>()))
            .ReverseMap();

        CreateMap<Quiz, QuizResponse>()
            .ForMember(dest => dest.QuestionResponse, opt => opt.MapFrom(src => src.QuizQuestions.Select(q => new QuestionResponse
            {
                Id = q.Question.Id,
                Content = q.Question.Content,
                OptionResponse = q.Question.Options.Any()
                    ? q.Question.Options.Select(o => new OptionResponse { Content = o.Content }).ToList()
                    : new List<OptionResponse>()
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

        CreateMap<Appointment, AppointmentFeedbackResponse>();
    }
}