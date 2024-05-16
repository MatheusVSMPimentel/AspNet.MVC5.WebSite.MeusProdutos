using System.Collections.Generic;

namespace MatheusVSMP.Business.Core.Notificacoes
{
    public interface INotificador
    {
        bool TemNotificacao();

        List<Notificacao> ObterNotificacoes();

        void Handle(Notificacao notificacao);
    }
}
