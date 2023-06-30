using AutoMapper;
using MeuMenu.Application.ViewModels.Base;
using MeuMenu.Application.ViewModels.Perfil;
using MeuMenu.Application.ViewModels.Usuario;
using MeuMenu.Domain.Models;
using MeuMenu.Domain.Utils;
using MeuMenu.Domain.ValueObjects;

namespace MeuMenu.Application.AutoMapper;

public class DomainParaViewModelProfile : Profile
{
    public DomainParaViewModelProfile()
    {
        CreateMap<Perfil, PerfilViewModel>();
        CreateMap(typeof(RetornoPaginadoValueObject<>), typeof(RetornoPaginadoViewModel<>));
        CreateMap<Usuario, UsuarioRetornoViewModel>()
            .ForMember(x => x.PerfilDescricao,
                opt => opt.MapFrom(src => src.PerfilId == null ? null : src.PerfilId.ObterDescricaoEnum()));
    }
}