

using SistemaRegistroPessoa.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SistemaRegistroPessoa.Interfaces
{
    public interface IRepository
    {
        Task Add(People people);
        Task Update(People people);
        Task Delete(People people);
        Task<IEnumerable<People>> GetAll();
        Task<People> GetById(Guid id);
        Task<IEnumerable<People>> GetPeoplesByUf(EnumUf uf);

        bool ExistCPF(string cpf);
        bool ExistCPF(Guid id, string cpf);
    }
}
