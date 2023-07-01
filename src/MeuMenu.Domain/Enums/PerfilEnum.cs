using System.ComponentModel;

namespace MeuMenu.Domain.Enums;

public enum PerfilEnum
{
    
    [Description("Administrador")]
    Adimin = 1,
    [Description("Usuário comum")]
    UsuarioComum = 2,
    Caixa = 3,
    Cozinha = 4,
    [Description("Garçom")]
    Garcom
}