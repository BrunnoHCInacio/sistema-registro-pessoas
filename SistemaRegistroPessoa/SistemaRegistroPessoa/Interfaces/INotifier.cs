using System;
using System.Collections.Generic;
using SistemaRegistroPessoa.Notificacoes;

namespace SistemaRegistroPessoa.Interfaces
{
    public interface INotifier : IDisposable
    {
        bool HasNotificationError();
        List<Notificacao> GetNotifications();
        void Handle(Notificacao notificacao);
    }
}
