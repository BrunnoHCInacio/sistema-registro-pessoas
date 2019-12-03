using System;
using System.Collections.Generic;
using System.Linq;
using SistemaRegistroPessoa.Interfaces;

namespace SistemaRegistroPessoa.Notifications
{
    public class Notifier : INotifier
    {
        List<Notification> _notifications;

        public Notifier()
        {
            _notifications = new List<Notification>();
        }

        public void Handle(Notification notificacao)
        {
            _notifications.Add(notificacao);
        }

        public List<Notification> GetNotifications()
        {
            return _notifications;
        }

        public bool HasNotification()
        {
            return _notifications.Any();
        }
    }
}
