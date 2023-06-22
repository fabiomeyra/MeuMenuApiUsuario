using System.ComponentModel;

namespace MeuMenu.Domain.Enums;

public enum PerfilEnum
{
    [Description("Usuário comum")]
    UsuarioComum = 1,
    [Description("Administrador")]
    Adimin
}