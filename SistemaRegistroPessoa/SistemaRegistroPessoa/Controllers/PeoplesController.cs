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
    public class PeoplesController : MainController
    {
        private readonly IRepository _repository;

        public PeoplesController(INotifier notifier,
                                 IRepository repository) : base(notifier)
        {
            _repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<Pessoa>> ObterTodos()
        {
            return await _repository.ObterTodos();
        }

        [HttpGet("obter-por-id/{id:guid}")]
        public async Task<ActionResult<Pessoa>> ObterPorId(Guid id)
        {
            var pessoa = await _repository.ObterPorId(id);
            if(pessoa == null)
            {
               return NotFound();
            }
            return pessoa;
        }

        [HttpPost]
        public async Task<ActionResult> Adicionar(Pessoa pessoa)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            
            await _repository.Adicionar(pessoa);
            return CustomResponse(pessoa);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Atualizar(Guid id, Pessoa pessoa)
        {
            
            if(id != pessoa.Id)
            {
                NotifyError("O id informado não é o mesmo que foi passado na query");
                return CustomResponse(pessoa);
            }
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _repository.Atualizar(pessoa);
            return CustomResponse(pessoa);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Remover(Guid id)
        {
            var pessoa = await _repository.ObterPorId(id);
            if(pessoa == null)
            {
                return NotFound();
            }
            await _repository.Remover(pessoa.Id);
            Notify("Registro removido com sucesso");
            return CustomResponse();
        }

    }
}
