using AutoMapper;
using SmartSchool.API.DTOs;
using SmartSchool.API.Models;
namespace SmartSchool.API.Mapper;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Aluno, AlunoDto>()
            .ForMember(
             dest => dest.Nome,
             opt => opt.MapFrom(src => $"{src.Nome} {src.Sobrenome}")
             )
            .ForMember(
            dest => dest.Idade,
            opt => opt.MapFrom(src => src.DataNasc.GetCurrentAge()));

        CreateMap<AlunoDto, Aluno>();
        CreateMap<Aluno, AlunoRegistrarDto>().ReverseMap();

    }
}
