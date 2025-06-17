using Application.Dtos;
using AutoMapper;
using Domain.Entities;

namespace WebApi.Mappings;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<TaskEntity, TaskDto>().ReverseMap();
        CreateMap<UserEntity, RegisterDto>().ReverseMap();
    }
}
