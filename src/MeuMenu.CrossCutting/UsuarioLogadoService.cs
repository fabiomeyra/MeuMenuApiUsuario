using System.Text.Json;
using MeuMenu.Domain.Services.Utils;
using MeuMenu.Domain.ValueObjects;

namespace MeuMenu.CrossCutting;

public class UsuarioLogadoService : IUsuarioLogadoService
{
    private UsuarioLogadoValueObject? _usuarioLogado;

    public UsuarioLogadoValueObject? ObterUsuarioLogado() => _usuarioLogado;

    public void DefinirUsuarioLogado(string? usuarioString)
    {
        _usuarioLogado = usuarioString == null ? null : JsonSerializer.Deserialize<UsuarioLogadoValueObject>(usuarioString);
    }
}