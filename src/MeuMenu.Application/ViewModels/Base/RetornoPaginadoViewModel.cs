namespace MeuMenu.Application.ViewModels.Base;

public class RetornoPaginadoViewModel<T> where T : class
{
    public int QuantidadeTotalRegistros { get; set; }
    public ICollection<T> Lista { get; set; } = new List<T>();
}