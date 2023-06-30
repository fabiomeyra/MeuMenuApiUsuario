using AutoMapper;
using MeuMenu.Application.Filtros;
using MeuMenu.Application.ViewModels.Usuario;
using MeuMenu.Domain.Filtros;
using MeuMenu.Domain.Models;

namespace MeuMenu.Application.AutoMapper;

public class ViewModelParaDomainProfile : Profile
{
    public ViewModelParaDomainProfile()
    {
        CreateMap<UsuarioSalvarViewModel, Usuario>();

        // filtros
        CreateMap<PesquisarUsuarioFiltroViewModel, PesquisarUsuarioFiltro>();
    }
}