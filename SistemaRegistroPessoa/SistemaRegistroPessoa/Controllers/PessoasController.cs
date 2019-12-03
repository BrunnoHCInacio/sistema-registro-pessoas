using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaRegistroPessoa.Context;
using SistemaRegistroPessoa.Interfaces;
using SistemaRegistroPessoa.Models;


namespace SistemaRegistroPessoa.Controllers
{
    [Route("api/pessoas")]
    public class PessoasController : MainController
    {
        private readonly AppDbContext _context;
        private readonly IRepository _repository;

        public PessoasController(AppDbContext context,
                                 INotificador notificador,
                                 IRepository repository) : base(notificador)
        {
            _context = context;
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Pessoa>> ObterTodos()
        {
            return await _context.Pessoas.ToListAsync();
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<Pessoa>> ObterPorId(Guid id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if(pessoa == null)
            {
               return NotFound();
            }
            return pessoa;
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(Pessoa pessoa)
        {
            if (!ModelState.IsValid) CustomResponse(ModelState);
            
            await _repository.Adicionar(pessoa);
            return CustomResponse(pessoa);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, Pessoa pessoa)
        {
            if (!ModelState.IsValid) CustomResponse(ModelState);
            if(id != pessoa.Id)
            {
                return BadRequest();
            }
            await _repository.Atualizar(pessoa);
            return CustomResponse(pessoa);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            var pessoa = await _context.Pessoas.FindAsync(id);
            if(pessoa == null)
            {
                return NotFound();
            }
            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();
            return CustomResponse();
        }

    }
}
