using MeuMenu.Api.Controllers.Base;
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

    public UsuarioController(INotificador notificador, IUsuarioAppService appService) 
        : base(notificador)
    {
        _appService = appService;
    }

    /// <summary>
    /// Rota para buscar usuário por id
    /// </summary>
    /// <param name="usuarioId">Identificador do usuário</param>
    /// <returns>Retorna usuário buscado por id caso exista ou mensagem(s) de erro</returns>
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> Obter([FromRoute] Guid usuarioId)
    {
        var retorno = await _appService.Obter(usuarioId);
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
    /// Rota para adicionar um novo usuário
    /// </summary>
    /// <param name="usuario">Objeto a ser cadastrado</param>
    /// <returns>Usuario cadastrado acrescido de seu identificadr ou mensagem(s) de erro</returns>
    [HttpPost]
    public async Task<IActionResult> Adicionar([FromBody] UsuarioViewModel usuario)
    {
        var retorno = await _appService.Adicionar(usuario);
        return RespostaPadrao(retorno);
    }

    /// <summary>
    /// Rota para atualizar um novo usuário
    /// </summary>
    /// <param name="usuario">Objeto a ser atualizado</param>
    /// <returns>Usuario atualizado ou mensagem(s) de erro</returns>
    [HttpPut]
    public async Task<IActionResult> Atualizar([FromBody] UsuarioViewModel usuario)
    {
        var retorno = await _appService.Atualizar(usuario);
        return RespostaPadrao(retorno);
    }

    /// <summary>
    /// Rota para excluir um novo usuário
    /// </summary>
    /// <param name="usuarioId">identificador do objeto a ser excluído</param>
    /// <returns>Mensagem de erro ou mensagem(s) de erro</returns>
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Excluir([FromRoute] Guid usuarioId)
    {
        await _appService.Excluir(usuarioId);
        return RespostaPadrao();
    }
}