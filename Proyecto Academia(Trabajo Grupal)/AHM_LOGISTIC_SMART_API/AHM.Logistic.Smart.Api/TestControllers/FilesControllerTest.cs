using AHM.Logistic.Smart.BusinessLogic.Services;
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
    public class FilesControllerTest : ControllerBase
    {
        private readonly IMapper _mapper;
        public CustomerFileService _files = new CustomerFileService();
        public FilesControllerTest(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("List")]
        public IActionResult List()
        {
            var result = _files.List();
            return Ok(result);
        }

        [HttpGet("Details/{Id}")]
        public IActionResult Details(int Id)
        {
            var result = _files.Find(x => x.cfi_Id == Id);
            return Ok(result);
        }

        [HttpPost("Insert")]
        public IActionResult Insert(CustomerFilesModel items)
        {
            var item = _mapper.Map<tbCustomersFile>(items);
            var result = _files.Insert(item);
            if (!result.Success) return Conflict(result);
            return Ok(result);
        }

        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete(int Id, int Mod)
        {
            var result = _files.Delete(Id, Mod);
            return Ok(result);
        }
    }
}
