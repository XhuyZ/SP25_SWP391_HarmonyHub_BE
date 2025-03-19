using AutoMapper;
using Domain.DTOs.Requests;
using Domain.Entities;

namespace Domain.Automapper;

public class RequestMappingProfile : Profile
{
    public RequestMappingProfile()
    {
        CreateMap<RegisterMemberRequest, Account>()
            .ForMember(dest => dest.Email, opt =>
                opt.MapFrom(src => src.Email.ToLower()))
            .ForMember(dest => dest.HashedPassword, opt =>
                opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)));

        CreateMap<RegisterTherapistRequest, Account>()
            .ForMember(dest => dest.Email, opt =>
                opt.MapFrom(src => src.Email.ToLower()))
            .ForMember(dest => dest.HashedPassword, opt =>
                opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)));

        CreateMap<CreateBlogRequest, Blog>();

        CreateMap<CreatePackageRequest, Package>();

        CreateMap<CreateAppointmentRequest, Appointment>();

        CreateMap<CreateQuizRequest, Quiz>();

        CreateMap<UpdateAvailabilityRequest, Availability>();

        CreateMap<CreateReportRequest, Report>();

        CreateMap<UpdateReportRequest, Report>();

        CreateMap<UpdateBlogRequest, Blog>();

        CreateMap<AddQualificationRequest, Qualification>();

        CreateMap<CreateSpecialtyRequest, Specialty>();

        CreateMap<UpdateSpecialtyRequest, Specialty>();
    }
}