using MeuMenu.Domain.Enums;
using MeuMenu.Domain.Interfaces.Utilitarios;
using MeuMenu.Domain.Models.Base;

namespace MeuMenu.Domain.Models;

public class Usuario : EntidadeValidavelModel<Usuario>
{
    public Guid UsuarioId { get; set; }
    public string? UsuarioNome { get; set; }
    public string? UsuarioLogin { get; set; }
    public string? UsuarioSenha { get; set; }
    public PerfilEnum? PerfilId { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAlteracao { get; set; }

    public Usuario GerarNovoId()
    {
        UsuarioId = Guid.NewGuid();
        return this;
    }

    public Usuario CriptografarSenha(IServicoDeCriptografia servicoCriptografia)
    {
        UsuarioSenha = string.IsNullOrWhiteSpace(UsuarioSenha)
            ? UsuarioSenha
            : servicoCriptografia.Criptografar(UsuarioSenha);
        return this;
    }
}