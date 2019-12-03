﻿using System;
using System.Collections.Generic;
using SistemaRegistroPessoa.Notificacoes;

namespace SistemaRegistroPessoa.Interfaces
{
    public interface INotificador
    {
        bool TemNotificacao();
        List<Notificacao> ObterNotificacoes();
        void Handle(Notificacao notificacao);
    }
}
