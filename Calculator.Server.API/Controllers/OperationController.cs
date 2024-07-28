﻿using Calculator.Data.Models;
using Calculator.Data.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Calculator.Server.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OperationController : ControllerBase
    {

        private readonly IOperationRepository operationRepository;

        public OperationController(IOperationRepository operationRepository)
        {
            this.operationRepository = operationRepository;
        }

        // GET: Operation
        [HttpGet]
        public IEnumerable<Operation> GetAll()
        {
            return operationRepository.ReadAll();
        }

        // POST: Operation
        [HttpPost]
        public ActionResult<Operation> Create(Operation operation)
        {
            return operationRepository.Create(operation);
        }

        // DELETE: Operation/id
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            operationRepository.Delete(id);
            return Ok();
        }

        //// GET: Operation/id
        //[HttpGet("{id}")]
        //public ActionResult<Operation> GetOne(int id)
        //{
        //    Operation? operation = operationRepository.ReadOne(id);
        //    if (operation == null)
        //    {
        //        return NotFound();
        //    }
        //    return operation;
        //}

    }
}
