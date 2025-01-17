using AutoMapper;
using Domain.DTOs.Requests;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Automapper
{
    public class CreateBlogMappingProfile : Profile
    {
        public CreateBlogMappingProfile()
        {
            CreateMap<CreateBlogRequest, Blog>();
        }
    }
}
