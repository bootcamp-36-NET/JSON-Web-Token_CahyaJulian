using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LearnNetCore.Repositories.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LearnNetCore.Base
{
    [Authorize(AuthenticationSchemes ="Bearer ")]
    [Route("api/[controller]")]
    [ApiController]
    public class BasesController<TEntity, TRepository> : ControllerBase
        where TEntity:class
        where TRepository:IRepository<TEntity>
    {
        private IRepository<TEntity> _repository;

        public BasesController(TRepository repository)
        {
            this._repository = repository;
        }

        [HttpGet]
        public async Task<IEnumerable<TEntity>> GetAll()=> await _repository.GetAll();

        [HttpGet("{id}")]
        public async Task<ActionResult<TEntity>> GetId(int id) => await _repository.GetById(id); 

        [HttpPost]
        public async Task<ActionResult<TEntity>> Create (TEntity entity)
        {
            var data = await _repository.Create(entity);
            if (data > 0)
            {
                return Ok("Data saved");
            }
            return BadRequest("Failed to input data. Please try again");
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult<int>> Delete (int Id)
        {
            var delete = await _repository.Delete(Id);
            if (delete.Equals(null))
            {
                return NotFound("Data doesn't found");

            }
            return delete;
        }
    }
}
