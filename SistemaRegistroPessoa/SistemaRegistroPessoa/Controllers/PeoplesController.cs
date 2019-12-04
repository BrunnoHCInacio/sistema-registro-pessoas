using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaRegistroPessoa.Context;
using SistemaRegistroPessoa.Interfaces;
using SistemaRegistroPessoa.Models;


namespace SistemaRegistroPessoa.Controllers
{
    [Authorize]
    [Route("api/peoples")]
    public class PeoplesController : MainController
    {
        private readonly IRepository _repository;
        private readonly IServices _service;

        public PeoplesController(INotifier notifier,
                                 IRepository repository, 
                                 IServices service) : base(notifier)
        {
            _repository = repository;
            _service = service;
        }

        [HttpGet]
        public async Task<IEnumerable<People>> GetAll()
        {
            return await _repository.GetAll();
        }

        [HttpGet("get-by-id/{id:guid}")]
        public async Task<ActionResult<People>> GetById(Guid id)
        {
            var pessoa = await _repository.GetById(id);
            if(pessoa == null)
            {
               return NotFound();
            }
            return pessoa;
        }

        [HttpGet("get-peoples-by-uf/{uf}")]
        public async Task<IEnumerable<People>> GetPeoplesByUf(EnumUf uf)
        {
            return await _repository.GetPeoplesByUf(uf);
        }

        [HttpPost]
        public async Task<ActionResult> Add(People people)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            await _service.Add(people);
            
            return CustomResponse(people);
        }

        [HttpPut("{id:guid}")]
        public async Task<ActionResult> Update(Guid id, People people)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);
            await _service.Update(people);
            return CustomResponse(people);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var people = await _repository.GetById(id);
            if(people == null)
            {
                return NotFound();
            }
            await _repository.Delete(people);
            return CustomResponse();
        }

    }
}
