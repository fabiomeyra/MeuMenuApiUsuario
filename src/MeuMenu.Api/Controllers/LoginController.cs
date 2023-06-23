using MeuMenu.Api.Controllers.Base;
using MeuMenu.Api.Infra;
using MeuMenu.Application.Interfaces;
using MeuMenu.Application.ViewModels.Usuario;
using MeuMenu.Domain.Interfaces.Notificador;
using Microsoft.AspNetCore.Mvc;

namespace MeuMenu.Api.Controllers;

[Route("api/login")]
public class LoginController : BaseController
{
    private readonly IUsuarioAppService _usuarioAppService;
    private readonly IPerfilAppService _perfilAppService;
    private readonly JwtService _jwtService;
    private readonly CriptografiaLoginService _criptografiaLoginService;

    public LoginController(INotificador notificador, 
        IPerfilAppService perfilAppService, 
        IUsuarioAppService usuarioAppService, 
        JwtService jwtService, CriptografiaLoginService criptografiaLoginService) 
        : base(notificador)
    {
        _perfilAppService = perfilAppService;
        _usuarioAppService = usuarioAppService;
        _jwtService = jwtService;
        _criptografiaLoginService = criptografiaLoginService;
    }

    /// <summary>
    /// Rota para autenticação na api
    /// </summary>
    /// <param name="usuario">Objeto para validação de credenciais</param>
    /// <returns>Token JWT ou mensagem(s) de erro</returns>
    [HttpPost]
    public async Task<IActionResult> Logar([FromBody] UsuarioLoginViewModel usuario)
    {
        usuario.Senha = _criptografiaLoginService.Descriptografar(usuario.Senha);
        var usuarioLogado = await _usuarioAppService.RealizarLogin(usuario);
        if (usuarioLogado is null) return RespostaPadrao(null);
        var perfil = await _perfilAppService.Obter(usuarioLogado.PerfilId!.Value);
        return RespostaPadrao(_jwtService.GerarToken(usuarioLogado, perfil));
    }
}