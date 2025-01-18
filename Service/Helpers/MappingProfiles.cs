using AutoMapper;
using Domain.DTOs.Responses;
using Domain.Entities;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // ... các mapping khác ...
        
        CreateMap<Account, ProfileResponse>()
            .ForMember(dest => dest.Gender, 
                opt => opt.MapFrom(src => src.Gender == 1 ? "Male" : "Female"));
    }
} 