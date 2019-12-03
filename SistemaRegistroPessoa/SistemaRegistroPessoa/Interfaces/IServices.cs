using SistemaRegistroPessoa.Models;
using System;
using System.Threading.Tasks;

namespace SistemaRegistroPessoa.Interfaces
{
    public interface IServices
    {
        Task Add(People people);
        Task Update(People people);
    }
}
