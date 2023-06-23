using AutoMapper;
using MeuMenu.Application.AppServices.Base;
using MeuMenu.Application.Interfaces;
using MeuMenu.Application.ViewModels.Usuario;
using MeuMenu.Domain.Interfaces.Notificador;
using MeuMenu.Domain.Interfaces.Services;
using MeuMenu.Domain.Interfaces.Utilitarios;
using MeuMenu.Domain.Models;
using MeuMenu.Domain.Notificador;
using MeuMenu.Domain.Services.Base;
using MeuMenu.Domain.UoW;

namespace MeuMenu.Application.AppServices;

public class UsuarioAppService : BaseAppService, IUsuarioAppService
{
    private readonly IUsuarioService _service;
    private readonly INotificador _notificador;
    private readonly IServicoDeCriptografia _servicoDeCriptografia;

    public UsuarioAppService(
        IUnitOfWork ouw, 
        NegocioService negocioService, 
        IMapper mapper, 
        IUsuarioService usuarioService, 
        INotificador notificador, 
        IServicoDeCriptografia servicoDeCriptografia) 
        : base(ouw, negocioService, mapper)
    {
        _service = usuarioService;
        _notificador = notificador;
        _servicoDeCriptografia = servicoDeCriptografia;
    }

    public async Task<UsuarioRetornoViewModel?> RealizarLogin(UsuarioLoginViewModel usuarioVm)
    {
        if (!usuarioVm.PreencheuLoignSenha())
        {
            _notificador.AdicionarNotificacao(new Notificacao("Deve-se informar o login e senha"));
            return null;
        }

        usuarioVm.CriptografarSenha(_servicoDeCriptografia);
        var retorno = await _service.Obter(x => x.UsuarioSenha == usuarioVm.Senha
            && x.UsuarioLogin == usuarioVm.Login, x => new Usuario
        {
            UsuarioId = x.UsuarioId,
            UsuarioNome = x.UsuarioNome,
            UsuarioLogin = x.UsuarioLogin,
            PerfilId = x.PerfilId
        });
        if (retorno is not null) return Mapper.Map<UsuarioRetornoViewModel>(retorno);
        _notificador.AdicionarNotificacao(new Notificacao("Usuário e/ou senha inválido(s)"));
        return null;

    }

    public async Task<ICollection<UsuarioRetornoViewModel>> BuscarTodos()
    {
        var retorno = await _service.Buscar(x => true, x => new Usuario
        {
            UsuarioNome = x.UsuarioNome,
            PerfilId = x.PerfilId,
            UsuarioId = x.UsuarioId,
            UsuarioLogin = x.UsuarioLogin
        });
        return Mapper.Map<ICollection<UsuarioRetornoViewModel>>(retorno);
    }

    public async Task<UsuarioRetornoViewModel> Obter(Guid usuarioId)
    {
        var retorno = await _service.Obter(x => x.UsuarioId == usuarioId, x => new Usuario
        {
            UsuarioNome = x.UsuarioNome,
            PerfilId = x.PerfilId,
            UsuarioId = x.UsuarioId,
            UsuarioLogin = x.UsuarioLogin
        });
        return Mapper.Map<UsuarioRetornoViewModel>(retorno);
    }

    public async Task<UsuarioRetornoViewModel> Adicionar(UsuarioSalvarViewModel usuarioVm)
    {
        if (!usuarioVm.SenhasIguais()) return RetornaSenhasNaoSaoIguais();
        var model = Mapper.Map<Usuario>(usuarioVm);
        model = await _service.Adicionar(model);
        await Commit();
        return Mapper.Map<UsuarioRetornoViewModel>(model);
    }

    public async Task<UsuarioRetornoViewModel> Atualizar(UsuarioSalvarViewModel usuarioVm)
    {
        if (!usuarioVm.SenhasIguais()) return RetornaSenhasNaoSaoIguais();
        var model = Mapper.Map<Usuario>(usuarioVm);
        model = await _service.Atualizar(model);
        await Commit();
        return Mapper.Map<UsuarioRetornoViewModel>(model);
    }

    public async Task Excluir(Guid usuarioId)
    {
        var model = new Usuario { UsuarioId = usuarioId };
        await _service.Excluir(model);
        await Commit();
    }

    private UsuarioRetornoViewModel RetornaSenhasNaoSaoIguais()
    {
        _notificador.AdicionarNotificacao(new Notificacao("As senhas não conferem"));
        return null!;
    }
}