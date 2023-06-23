using MeuMenu.Domain.Interfaces.Utilitarios;

namespace MeuMenu.Application.ViewModels.Usuario;

public class UsuarioLoginViewModel
{
    public string? Login { get; set; }
    public string? Senha { get; set; }

    public bool PreencheuLoignSenha() => !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Senha);

    public UsuarioLoginViewModel CriptografarSenha(IServicoDeCriptografia servicoCriptografia)
    {
        if (!PreencheuLoignSenha()) return this;
        Senha = servicoCriptografia.Criptografar(Senha!);
        return this;
    }
}