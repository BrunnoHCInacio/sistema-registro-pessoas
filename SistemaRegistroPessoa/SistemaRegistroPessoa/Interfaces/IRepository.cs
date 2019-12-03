using System;
using System.Threading.Tasks;
using SistemaRegistroPessoa.Models;

namespace SistemaRegistroPessoa.Interfaces
{
    public interface IRepository
    {
        Task Adicionar(Pessoa pesssoa);
        Task Atualizar(Pessoa pessoa);
    }
}
