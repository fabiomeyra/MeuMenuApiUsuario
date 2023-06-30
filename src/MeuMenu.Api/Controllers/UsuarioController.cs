using MeuMenu.Api.Controllers.Base;
using MeuMenu.Api.Infra;
using MeuMenu.Application.Filtros;
using MeuMenu.Application.Interfaces;
using MeuMenu.Application.ViewModels.Usuario;
using MeuMenu.Domain.Interfaces.Notificador;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeuMenu.Api.Controllers;

[Authorize("Bearer")]
[Route("api/usuario")]
public class UsuarioController : BaseController
{
    private readonly IUsuarioAppService _appService;
    private readonly IPerfilAppService _perfilAppService;
    private readonly CriptografiaLoginService _criptografiaLoginService;

    public UsuarioController(
        INotificador notificador, 
        IUsuarioAppService appService, 
        IPerfilAppService perfilAppService, 
        CriptografiaLoginService criptografiaLoginService) 
        : base(notificador)
    {
        _appService = appService;
        _perfilAppService = perfilAppService;
        _criptografiaLoginService = criptografiaLoginService;
    }

    /// <summary>
    /// Rota para buscar lista de perfis para lista de seleção
    /// </summary>
    /// <returns>Retorna lista de perfis usuário ou mensagem(s) de erro</returns>
    [HttpGet("perfis-para-selecao")]
    public IActionResult BuscaListaPerfisParaSelecao()
    {
        var retorno = _perfilAppService.BuscarParaSelecao();
        return RespostaPadrao(retorno);
    }

    /// <summary>
    /// Rota para buscar usuário por id
    /// </summary>
    /// <param name="id">Identificador do usuário</param>
    /// <returns>Retorna usuário buscado por id caso exista ou mensagem(s) de erro</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Obter([FromRoute] Guid id)
    {
        var retorno = await _appService.Obter(id);
        return RespostaPadrao(retorno);
    }

    /// <summary>
    /// Rota para buscar todos os usuários
    /// </summary>
    /// <returns>Retorna lista de usuários ou mensagem(s) de erro</returns>
    [HttpGet]
    public async Task<IActionResult> BuscarTodos()
    {
        var retorno = await _appService.BuscarTodos();
        return RespostaPadrao(retorno);
    }

    /// <summary>
    /// Rota para pesquisar usuários
    /// </summary>
    /// /// <param name="filtro">Filtro a ser aplicado na busca</param>
    /// <returns>Retorna lista de usuários filtrados pelo valor passado no parâmetro filtro ou mensagem(s) de erro</returns>
    [HttpGet("pesquisar")]
    public async Task<IActionResult> Pesquisar([FromQuery] PesquisarUsuarioFiltroViewModel filtro)
    {
        var retorno = await _appService.Pesquisar(filtro);
        return RespostaPadrao(retorno);
    }

    /// <summary>
    /// Rota para adicionar um novo usuário
    /// </summary>
    /// <param name="usuario">Objeto a ser cadastrado</param>
    /// <returns>Usuario cadastrado acrescido de seu identificadr ou mensagem(s) de erro</returns>
    [HttpPost]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Adicionar([FromBody] UsuarioSalvarViewModel usuario)
    {
        usuario = DescriptografarSenhas(usuario);
        var retorno = await _appService.Adicionar(usuario);
        return RespostaPadrao(retorno);
    }

    /// <summary>
    /// Rota para atualizar um novo usuário
    /// </summary>
    /// <param name="usuario">Objeto a ser atualizado</param>
    /// <returns>Usuario atualizado ou mensagem(s) de erro</returns>
    [HttpPut]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Atualizar([FromBody] UsuarioSalvarViewModel usuario)
    {
        usuario = DescriptografarSenhas(usuario);
        var retorno = await _appService.Atualizar(usuario);
        return RespostaPadrao(retorno);
    }

    /// <summary>
    /// Rota para excluir um novo usuário
    /// </summary>
    /// <param name="id">identificador do objeto a ser excluído</param>
    /// <returns>Mensagem de erro ou mensagem(s) de erro</returns>
    [HttpDelete("{id:guid}")]
    [Authorize(Roles = "ADMIN")]
    public async Task<IActionResult> Excluir([FromRoute] Guid id)
    {
        await _appService.Excluir(id);
        return RespostaPadrao();
    }

    private UsuarioSalvarViewModel DescriptografarSenhas(UsuarioSalvarViewModel usuario)
    {
        usuario.UsuarioSenha = _criptografiaLoginService.Descriptografar(usuario.UsuarioSenha);
        usuario.UsuarioSenhaConfirmacao = _criptografiaLoginService.Descriptografar(usuario.UsuarioSenhaConfirmacao);
        return usuario;
    }
}