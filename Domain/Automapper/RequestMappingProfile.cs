using AutoMapper;
using Domain.DTOs.Requests;
using Domain.Entities;

namespace Domain.Automapper;

public class RequestMappingProfile: Profile
{
    public RequestMappingProfile()
    {
        CreateMap<CreateAccountRequest, Account>();
    }
}