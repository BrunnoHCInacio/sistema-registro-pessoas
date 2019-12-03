using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SistemaRegistroPessoa.Interfaces;
using SistemaRegistroPessoa.Notificacoes;

namespace SistemaRegistroPessoa.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INotificador _notificador;

        protected MainController(INotificador notificador)
        {
            _notificador = notificador;
        }

        protected bool OperacaoValida()
        {
            return !_notificador.TemNotificacao();
        }



        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if (!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();

        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in erros)
            {
                var errorMsg = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NofificarErro(errorMsg);
            }
        }

        protected ActionResult CustomResponse(object obj = null)
        {
            if (OperacaoValida())
            {
                return Ok(new
                {
                    success = true,
                    data = obj
                });
            }
            return BadRequest(new
            {
                success = false,
                data = _notificador.ObterNotificacoes().Select(n => n.Message)
            });
        }

        protected void NofificarErro(string message)
        {
            _notificador.Handle(new Notificacao(message));
        }
    }
}