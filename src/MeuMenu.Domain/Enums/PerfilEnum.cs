using System.ComponentModel;

namespace MeuMenu.Domain.Enums;

public enum PerfilEnum
{
    
    [Description("Administrador")]
    Adimin = 1,
    [Description("Usuário comum")]
    UsuarioComum = 2,
    [Description("Caixa")]
    Caixa = 3,
    [Description("Cozinha")]
    Cozinha = 4
}