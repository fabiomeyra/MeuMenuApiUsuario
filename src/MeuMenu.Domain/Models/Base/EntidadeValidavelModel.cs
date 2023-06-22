namespace MeuMenu.Domain.Models.Base;

public abstract class EntidadeValidavelModel<TE> where TE : class
{
    private readonly Guid _entidadeValidavelId;

    protected EntidadeValidavelModel()
    {
        _entidadeValidavelId = Guid.NewGuid();
    }

    public Guid ObterGuidEntidade() => _entidadeValidavelId;

    //private readonly ICollection<AbstractValidator<TE>> _validacoes = new List<AbstractValidator<TE>>();

    //public EntidadeValidavelModel<TE> AdicionarValidacao(AbstractValidator<TE> validacao)
    //{
    //    _validacoes.Add(validacao);
    //    return this;
    //}

    //public ICollection<AbstractValidator<TE>> ObterValidacoes() => _validacoes;

    public virtual TE LimparPropriedadesNavegacao()
    {
        return null!;
    }
}