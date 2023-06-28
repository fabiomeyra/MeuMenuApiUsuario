using MeuMenu.Domain.Interfaces.Utilitarios;
using System.Diagnostics.CodeAnalysis;

namespace MeuMenu.CrossCutting;

[ExcludeFromCodeCoverage]
public class ServicoDeCriptografia : IServicoDeCriptografia
{
    
    /// <summary>
    ///     Método responsável por Criptografar um valor passado como parâmetro.
    /// </summary>
    /// <param name="value">Valor a ser criptografado.</param>
    /// <returns>Valor criptografado.</returns>
    public string Criptografar(string value)
    {
        return ServicoDeCriptografiaStatic.Criptografar(value);
    }

    /// <summary>
    ///     Método responsável por Criptografar um valor passado como parâmetro.
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public string Descriptografar(string value)
    {
        return ServicoDeCriptografiaStatic.Descriptografar(value);
    }
}