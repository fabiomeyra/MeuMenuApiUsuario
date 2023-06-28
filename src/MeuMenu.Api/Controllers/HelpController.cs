using MeuMenu.Domain.Interfaces.Utilitarios;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace MeuMenu.Api.Controllers;

[Authorize("Bearer")]
[Route("api/help")]
public class HelpController : ControllerBase
{
    private readonly IServicoDeCriptografia _servicoDeCriptografia;

    public HelpController(IServicoDeCriptografia servicoDeCriptografia)
    {
        _servicoDeCriptografia = servicoDeCriptografia;
    }

    [HttpGet("criptografar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = "ADMIN")]
    public IActionResult Criptografar([FromBody] string valor)
    {
        var list = valor.Split("||");
        var listEncryp = new string[list.Length];
        for (int i = 0; i < list.Length; i++)
        {
            listEncryp[i] = _servicoDeCriptografia.Criptografar(list[i]);
        }
        return Ok(listEncryp);
    }

    [HttpGet("descriptografar")]
    [ApiExplorerSettings(IgnoreApi = true)]
    [Authorize(Roles = "ADMIN")]
    public IActionResult Descriptografar([FromBody] string valor)
    {
        // Request
        var dec = _servicoDeCriptografia.Descriptografar(valor);
        return Ok(dec);
    }
}