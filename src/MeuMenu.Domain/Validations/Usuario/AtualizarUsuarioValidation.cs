using FluentValidation;
using MeuMenu.Domain.Interfaces.Services;

namespace MeuMenu.Domain.Validations.Usuario;

public class AtualizarUsuarioValidation : AbstractValidator<Models.Usuario>
{
    private readonly IUsuarioService _usuarioService;

    public AtualizarUsuarioValidation(IUsuarioService usuarioService)
    {
        _usuarioService = usuarioService;
        ValidarCamposObrigatorios();
        VerificarLoginUnico();
    }

    private void VerificarLoginUnico()
    {
        RuleFor(x => x)
            .MustAsync(async (x, _) =>
            {
                var usuario = await _usuarioService.Obter(y => y.UsuarioLogin == x.UsuarioLogin
                    && y.UsuarioId != x.UsuarioId, y => y.UsuarioLogin);
                return string.IsNullOrWhiteSpace(usuario);
            })
            .When(CamposObrigatoriosPreenchidos)
            .WithMessage(RetornaMensagemFormatado(MensagensValidacaoResources.LoginJaExistente));
    }

    private void ValidarCamposObrigatorios()
    {
        RuleFor(x => x.UsuarioId)
            .Must(x => x != Guid.Empty)
            .WithMessage(RetornaMensagemFormatado(MensagensValidacaoResources.DeveInformarIdentificadorAtualizar));

        RuleFor(x => x.UsuarioNome)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage(RetornaMensagemFormatado(MensagensValidacaoResources.NomeObrigatorio));

        RuleFor(x => x.UsuarioLogin)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage(RetornaMensagemFormatado(MensagensValidacaoResources.LoginObrigatorio));

        RuleFor(x => x.UsuarioSenha)
            .Must(x => !string.IsNullOrWhiteSpace(x))
            .WithMessage(RetornaMensagemFormatado(MensagensValidacaoResources.SenhaObrigatoria));
    }

    private bool CamposObrigatoriosPreenchidos(Models.Usuario usuario)
    {
        return
            !string.IsNullOrWhiteSpace(usuario.UsuarioNome)
            && !string.IsNullOrWhiteSpace(usuario.UsuarioLogin)
            && !string.IsNullOrWhiteSpace(usuario.UsuarioSenha)
            && usuario.UsuarioId != Guid.Empty;
    }

    private string RetornaMensagemFormatado(string mensage) => $"(Usuário): {mensage}";
}