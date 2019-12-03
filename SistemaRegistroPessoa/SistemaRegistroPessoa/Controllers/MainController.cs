using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using SistemaRegistroPessoa.Interfaces;
using SistemaRegistroPessoa.Notifications;

namespace SistemaRegistroPessoa.Controllers
{
    [ApiController]
    public class MainController : ControllerBase
    {
        private readonly INotifier _notifier;

        protected MainController(INotifier notifier)
        {
            _notifier = notifier;
        }

        protected bool OperationValid()
        {
            return !_notifier.HasNotification();
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
                NotifyError(errorMsg);
            }
        }

        protected ActionResult CustomResponse(object obj = null)
        {
            if (OperationValid())
            {
                return Ok(new
                {
                    success = true,
                    data = obj == null ? _notifier.GetNotifications().Select(n => n.Message) : obj
                });
            }
            return BadRequest(new
            {
                success = false,
                data = _notifier.GetNotifications().Select(n => n.Message)
            });
        }

        protected void NotifyError(string message)
        {
            _notifier.Handle(new Notification(message));
        }
    }
}