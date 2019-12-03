using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaRegistroPessoa.Context;
using SistemaRegistroPessoa.Interfaces;
using SistemaRegistroPessoa.Models;

namespace SistemaRegistroPessoa.Repository
{
    public class PessoaRepository : IRepository
    {
        private readonly AppDbContext _context;

        public PessoaRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Adicionar(Pessoa pessoa)
        {
            pessoa.Id = Guid.NewGuid();
            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();
        }

        public async Task Atualizar(Pessoa pessoa)
        {
            _context.Pessoas.Update(pessoa);
            await _context.SaveChangesAsync();
        }

        public async Task<Pessoa> ObterPorId(Guid id)
        {
            return await _context.Pessoas.FindAsync(id);
        }

        public async Task<IEnumerable<Pessoa>> ObterTodos()
        {
            return await _context.Pessoas.ToListAsync();
        }

        public async Task Remover(Guid id)
        {
            var pessoa = _context.Pessoas.Find(id);
            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
        }
    }
}
