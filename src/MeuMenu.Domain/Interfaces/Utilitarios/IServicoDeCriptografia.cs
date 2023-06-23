namespace MeuMenu.Domain.Interfaces.Utilitarios;

public interface IServicoDeCriptografia
{
    public string Criptografar(string value);
    public string Descriptografar(string value);
}