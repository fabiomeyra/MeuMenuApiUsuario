using MeuMenu.Domain.ValueObjects;

namespace MeuMenu.Domain.Services.Utils;

public interface IUsuarioLogadoService
{
    UsuarioLogadoValueObject? ObterUsuarioLogado();
    void DefinirUsuarioLogado(string? usuarioString);
}