namespace MeuMenu.Domain.ValueObjects;

public class RetornoPaginadoValueObject<T> where T : class
{
    public int QuantidadeTotalRegistros { get; set; }
    public ICollection<T> Lista { get; set; } = new List<T>();
}