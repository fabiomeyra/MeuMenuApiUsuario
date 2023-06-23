namespace MeuMenu.CrossCutting.AppSettings;

public abstract class BaseAppSettings
{
    protected string? RetornaValorDescriptografado(string? valor)
    {
        if (valor == null) return null;
        try
        {
            return ServicoDeCriptografiaStatic.Descriptografar(valor);
        }
        catch
        {
            return valor;
        }
    }
}