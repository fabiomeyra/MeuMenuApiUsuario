namespace MeuMenu.Application.ViewModels.Usuario;

public class UsuarioViewModel
{
    public Guid UsuarioId { get; set; }
    public string? UsuarioNome { get; set; }
    public string? UsuarioLogin { get; set; }
    public string? UsuarioSenha { get; set; }
    public int? PerfilId { get; set; }
    public string? PerfilDescricao { get; set; }
}