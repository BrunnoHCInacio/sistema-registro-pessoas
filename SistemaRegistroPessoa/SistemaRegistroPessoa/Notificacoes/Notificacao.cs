using System;
namespace SistemaRegistroPessoa.Notificacoes
{
    public class Notificacao
    {
        public Notificacao(string message)
        {
            Message = message;
        }

        public string Message { get; set; }
    }
}
