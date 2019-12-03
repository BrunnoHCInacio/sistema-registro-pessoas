using SistemaRegistroPessoa.Interfaces;
using SistemaRegistroPessoa.Models;
using SistemaRegistroPessoa.Notifications;
using System;
using System.Threading.Tasks;

namespace SistemaRegistroPessoa.Services
{
    public class PeopleService: IServices
    {
        private readonly INotifier _notifier;
        private readonly IRepository _repository;

        public PeopleService(IRepository repository, INotifier notifier)
        {
            _repository = repository;
            _notifier = notifier;
        }

        public async Task Add(People people)
        {
            if (_repository.ExistCPF(people.Cpf))
            {
                Notify("Já existe um registro com este CPF");
                return;
            }
            await _repository.Add(people);
        }

        public async Task Update(People people)
        {
            if (_repository.ExistCPF(people.Id, people.Cpf))
            {
                Notify("Ja existe outro registro com o CPF informado");
                return;
            }
            await _repository.Update(people);
        }

        private void Notify(string message)
        {
            _notifier.Handle(new Notification(message));
        }
    }
}
