using AutoMapper;
using Domain.DTOs.Requests;
using Domain.Entities;

namespace Domain.Automapper;

public class RequestMappingProfile: Profile
{
    public RequestMappingProfile()
    {
        CreateMap<CreateAccountRequest, Account>()
            .ForMember(dest => dest.Email, opt =>
                opt.MapFrom(src => src.Email.ToLower()))
            .ForMember(dest => dest.HashedPassword, opt =>
                opt.MapFrom(src => BCrypt.Net.BCrypt.HashPassword(src.Password)));
    }
}