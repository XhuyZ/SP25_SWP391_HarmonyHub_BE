using AutoMapper;
using Domain.DTOs.Common;
using Domain.DTOs.Responses;
using Domain.Entities;

namespace Domain.Automapper;

public class ResponseMappingProfile: Profile
{
    public ResponseMappingProfile()
    {
        CreateMap(typeof(PagedList<>), typeof(PagedList<>));

        CreateMap<Account, AccountResponse>().ReverseMap();
    }
}