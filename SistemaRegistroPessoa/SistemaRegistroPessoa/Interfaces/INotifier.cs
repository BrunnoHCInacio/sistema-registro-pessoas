using System;
using System.Collections.Generic;
using SistemaRegistroPessoa.Notifications;

namespace SistemaRegistroPessoa.Interfaces
{
    public interface INotifier
    {
        bool HasNotification();
        List<Notification> GetNotifications();
        void Handle(Notification notification);
    }
}
