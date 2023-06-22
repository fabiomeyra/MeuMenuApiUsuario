using MeuMenu.Domain.Notificador;

namespace MeuMenu.Domain.Interfaces.Notificador;

public interface INotificador
{
    bool TemNotificacao();
    ICollection<Notificacao> ObterNotificacoes();
    void AdicionarNotificacao(Notificacao notificacao);
    void AdicionarNotificacao(ICollection<Notificacao> notificacoes);
    void LimparNotificacoes();
}