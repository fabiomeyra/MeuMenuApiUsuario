namespace MeuMenu.Application.ViewModels.Usuario;

public class UsuarioSalvarViewModel
{
    public Guid UsuarioId { get; set; }
    public string? UsuarioNome { get; set; }
    public string? UsuarioLogin { get; set; }
    public string? UsuarioSenha { get; set; }
    public string? UsuarioSenhaConfirmacao { get; set; }
    public int? PerfilId { get; set; }
    public string? PerfilDescricao { get; set; }

    public bool SenhasIguais() => UsuarioSenha == UsuarioSenhaConfirmacao
                                  && !string.IsNullOrWhiteSpace(UsuarioSenha);
}