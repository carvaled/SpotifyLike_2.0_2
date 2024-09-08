using Domain.Admin.Agreggates;
using Domain.Admin.ValueObject;
using Spotify.Application.Admin.Dto;

namespace Application.Administrative.Profile;
public class ContaAdminProfile : AutoMapper.Profile
{
    public ContaAdminProfile() 
    {
        CreateMap<ContaAdminDto, ContaAdmin>()
            .ForMember(dest => dest.PerfilType, opt =>  opt.MapFrom(src => new Perfil((Perfil.TipoUsuario)src.PerfilType)))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Senha, opt => opt.MapFrom(src => src.Senha))
            .ReverseMap();

        CreateMap<ContaAdmin, ContaAdminDto>()
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.PerfilType, opt => opt.MapFrom(src => (int)src.PerfilType.Id))
            .AfterMap((s, d) =>
            {
                d.Senha = "********";
            });

        CreateMap<Perfil, PerfilDto>().ReverseMap();
        CreateMap<PerfilDto, Perfil>().ReverseMap();
    }
}
