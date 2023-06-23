using MeuMenu.Domain.Interfaces.Repositories;
using MeuMenu.Domain.Interfaces.Services;
using MeuMenu.Domain.Interfaces.Utilitarios;
using MeuMenu.Domain.Models;
using MeuMenu.Domain.Services.Base;
using MeuMenu.Domain.Utils;
using MeuMenu.Domain.Validations.Usuario;

namespace MeuMenu.Domain.Services;

public class UsuarioService : BaseService<Usuario>, IUsuarioService
{
    private readonly IServicoDeCriptografia _servicoDeCriptografia;

    public UsuarioService(
        IUsuarioRepository repository, 
        NegocioService negocioService, 
        IServicoDeCriptografia servicoDeCriptografia) 
            : base(repository, negocioService)
    {
        _servicoDeCriptografia = servicoDeCriptografia;
    }

    public override Task<Usuario> Adicionar(Usuario objeto)
    {
        objeto
            .GerarNovoId()
            .CriptografarSenha(_servicoDeCriptografia)
            .AdicionarValidacaoEntidade(NegocioService, new AdicionarUsuarioValidation(this));
        return base.Adicionar(objeto);
    }

    public override Task<Usuario> Atualizar(Usuario objeto)
    {
        objeto
            .CriptografarSenha(_servicoDeCriptografia)
            .AdicionarValidacaoEntidade(NegocioService, new AtualizarUsuarioValidation(this));
        return base.Atualizar(objeto);
    }

    public override Task Excluir(Usuario objeto)
    {
        objeto.AdicionarValidacaoEntidade(NegocioService, new ExcluirUsuarioValidation());
        return base.Excluir(objeto);
    }
}