using FluentValidation;

namespace MeuMenu.Domain.Validations.Usuario;

public class ExcluirUsuarioValidation : AbstractValidator<Models.Usuario.Usuario>
{

    public ExcluirUsuarioValidation()
    {
        RuleFor(x => x.UsuarioId)
            .Must(x => x != Guid.Empty)
            .WithMessage(RetornaMensagemFormatado(MensagensValidacaoResources.DeveInformarIdentificadorAtualizar));
    }

    private string RetornaMensagemFormatado(string mensage) => $"(Usuário): {mensage}";
}