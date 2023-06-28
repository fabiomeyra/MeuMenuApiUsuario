using MeuMenu.Application.ViewModels.Perfil;
using MeuMenu.Application.ViewModels.Usuario;
using MeuMenu.CrossCutting.AppSettings;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace MeuMenu.Api.Infra;

public class JwtService
{
    private readonly AppSettings _appSettings;

    public JwtService(IOptions<AppSettings> opt)
    {
        _appSettings = opt.Value;
    }

    /// <summary>
    /// Gera o token
    /// </summary>
    /// <param name="usuario">Usuário logado</param>
    /// <param name="perfil">Perfil do usuário</param>
    /// <returns></returns>
    public dynamic GerarToken(UsuarioRetornoViewModel usuario, PerfilViewModel perfil)
    {
        usuario.Permissao = perfil.PerfilRole;

        var usuarioJson = JsonSerializer.Serialize(usuario, new JsonSerializerOptions
        {
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            WriteIndented = true
        });

        var handler = new JwtSecurityTokenHandler();

        var tokenDataExpiracao = _appSettings.Jwt?.RetornaHoraExpiracaoToken();

        var identity = new ClaimsIdentity(
            new GenericIdentity(usuario.UsuarioNome!, "User"),
            new []
            {
                new Claim("Login", usuario.UsuarioLogin!),
                new Claim("Usuario", usuarioJson)
            });

        if(!string.IsNullOrWhiteSpace(perfil.PerfilRole)) 
            identity.AddClaim(new Claim(ClaimTypes.Role, perfil.PerfilRole));

        var securityToken = handler.CreateToken(new SecurityTokenDescriptor
        {
            Issuer = _appSettings.Jwt?.Issuer,
            Audience = _appSettings.Jwt?.Audience,
            SigningCredentials = _appSettings.Jwt?.ObterSigningCredentials(),
            Subject = identity,
            NotBefore = DateTime.Now.AddMinutes(-1),
            Expires = tokenDataExpiracao
        }
        );

        var token = handler.WriteToken(securityToken);

        return new
        {
            AccessToken = token,
            ExpiresIn = tokenDataExpiracao,
            _appSettings.Jwt?.TokenType
        };
    }
}