using AHM.Logistic.Smart.BusinessLogic.TestService;
using AHM.Logistic.Smart.Common.Models;
using AHM.Logistic.Smart.Entities.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AHM.Logistic.Smart.Api.TestControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerNotesControllerTest : ControllerBase
    {
        public CustomerNotesService _clientService = new CustomerNotesService();
        private readonly IMapper _mapper;
        public CustomerNotesControllerTest(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("List")]
        public IActionResult List()
        {
            var result = _clientService.List();
            return Ok(result);
        }

        [HttpGet("Details/{Id}")]
        public IActionResult Details(int Id)
        {
            var result = _clientService.Find(x => x.cun_Id == Id);
            return Ok(result);
        }

        [HttpPost("Insert")]
        public IActionResult Insert(CustomerNotesModel items)
        {
            var item = _mapper.Map<tbCustomerNotes>(items);
            var result = _clientService.Insert(item);
            if (!result.Success) return Conflict(result);
            return Ok(result);
        }

        [HttpPut("Update/{Id}")]
        public IActionResult Update(int Id, CustomerNotesModel items)
        {
            var item = _mapper.Map<tbCustomerNotes>(items);
            var result = _clientService.Update(item, Id);
            return Ok(result);
        }

        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete(int Id, int Mod)
        {
            var result = _clientService.Delete(Id, Mod);
            return Ok(result);
        }
    }
}
