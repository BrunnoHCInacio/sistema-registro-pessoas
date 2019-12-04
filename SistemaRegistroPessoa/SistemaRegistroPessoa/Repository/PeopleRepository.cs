using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaRegistroPessoa.Context;
using SistemaRegistroPessoa.Interfaces;
using SistemaRegistroPessoa.Models;

namespace SistemaRegistroPessoa.Repository
{
    public class PeopleRepository : IRepository
    {
        private readonly AppDbContext _context;

        public PeopleRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task Add(People people)
        {
            _context.Peoples.Add(people);
            await _context.SaveChangesAsync();
        }

        public async Task Update(People people)
        {
            _context.Peoples.Update(people);
            await _context.SaveChangesAsync();
        }

        public async Task<People> GetById(Guid id)
        {
            return await _context.Peoples.FindAsync(id);
        }

        public async Task<IEnumerable<People>> GetAll()
        {
            return await _context.Peoples.ToListAsync();
        }

        public async Task Delete(People people)
        {
            _context.Peoples.Remove(people);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<People>> GetPeoplesByUf(EnumUf uf)
        {
            return await _context.Peoples.Where(p => p.Uf == uf).ToListAsync();
        }

        public bool ExistCPF(string cpf)
        {
            var people = _context.Peoples.FirstOrDefault(p => p.Cpf == cpf);
            if (people != null)
            {
                return true;
            }
            return false;
        }

        public bool ExistCPF(Guid id,string cpf)
        {
            var people = _context.Peoples.FirstOrDefault(p => p.Cpf == cpf);
            if (people != null && people.Id != id)
            {
                return true;
            }
            return false;
        }
    }
}
