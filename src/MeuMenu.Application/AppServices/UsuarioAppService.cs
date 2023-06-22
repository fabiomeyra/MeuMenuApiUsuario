using AutoMapper;
using MeuMenu.Application.AppServices.Base;
using MeuMenu.Application.Interfaces;
using MeuMenu.Application.ViewModels.Usuario;
using MeuMenu.Domain.Interfaces.Services;
using MeuMenu.Domain.Models.Usuario;
using MeuMenu.Domain.Services.Base;
using MeuMenu.Domain.UoW;

namespace MeuMenu.Application.AppServices;

public class UsuarioAppService : BaseAppService, IUsuarioAppService
{
    private readonly IUsuarioService _service;

    public UsuarioAppService(
        IUnitOfWork ouw, 
        NegocioService negocioService, 
        IMapper mapper, 
        IUsuarioService usuarioService) 
        : base(ouw, negocioService, mapper)
    {
        _service = usuarioService;
    }

    public async Task<ICollection<UsuarioViewModel>> BuscarTodos()
    {
        var retorno = await _service.Buscar(x => true);
        return Mapper.Map<ICollection<UsuarioViewModel>>(retorno);
    }

    public async Task<UsuarioViewModel> Obter(Guid usuarioId)
    {
        var retorno = await _service.Obter(x => x.UsuarioId == usuarioId);
        return Mapper.Map<UsuarioViewModel>(retorno);
    }

    public async Task<UsuarioViewModel> Adicionar(UsuarioViewModel usuarioVm)
    {
        var model = Mapper.Map<Usuario>(usuarioVm);
        model = await _service.Adicionar(model);
        await Commit();
        return Mapper.Map<UsuarioViewModel>(model);
    }

    public async Task<UsuarioViewModel> Atualizar(UsuarioViewModel usuarioVm)
    {
        var model = Mapper.Map<Usuario>(usuarioVm);
        model = await _service.Atualizar(model);
        await Commit();
        return Mapper.Map<UsuarioViewModel>(model);
    }

    public async Task Excluir(Guid usuarioId)
    {
        var model = new Usuario { UsuarioId = usuarioId };
        await _service.Excluir(model);
        await Commit();
    }
}