using AutoMapper;
using Domain.DTOs.Common;
using Domain.DTOs.Requests;
using Domain.DTOs.Responses;
using Domain.Entities;

namespace Domain.Automapper;

public class ResponseMappingProfile: Profile
{
    public ResponseMappingProfile()
    {
        CreateMap(typeof(PagedList<>), typeof(PagedList<>));

        CreateMap<Account, AccountResponse>().ReverseMap();
        
        CreateMap<Blog, BlogResponse>()
            .ForMember(dest => dest.BlogId, opt => opt.MapFrom(src => src.Id)).ReverseMap();
            
        CreateMap<Account, LoginResponse>()
            .ForMember(dest => dest.AccountId, opt =>
                opt.MapFrom(src => src.Id))
            .ReverseMap();

        CreateMap<Account, TherapistDetailsResponse>().ReverseMap();
        CreateMap<Availability, AvailabilityResponse>().ReverseMap();
        CreateMap<Qualification, QualificationResponse>().ReverseMap();
        CreateMap<Specialty, SpecialtyResponse>().ReverseMap();
    }
}