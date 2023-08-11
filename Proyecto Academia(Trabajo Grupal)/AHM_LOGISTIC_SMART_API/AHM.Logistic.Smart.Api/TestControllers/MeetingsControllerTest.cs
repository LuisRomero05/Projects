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
    public class MeetingsControllerTest : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly MeetingsService _meetingsServices = new MeetingsService();
        public MeetingsControllerTest(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet("List")]
        public IActionResult List()
        {
            var result = _meetingsServices.ListMeeting();
            return Ok(result);
        }

        [HttpGet("ListCusEmp")]
        public IActionResult ListCusEmp()
        {
            var result = _meetingsServices.ListInvEmp();
            return Ok(result);
        }

        [HttpGet("MeetingsDetails/{Id}")]
        public IActionResult MeetingsDetails(int Id)
        {
            var result = _meetingsServices.DetailsMeetings(x => x.met_Id == Id && x.mde_Status == true);
            return Ok(result);
        }

        [HttpPost("Insert")]
        public IActionResult Insert(MeetingsModel items)
        {
            var item = _mapper.Map<tbMeetings>(items);
            var data = _mapper.Map<List<tbMeetingsDetails>>(items.met_details);
            var result = _meetingsServices.RegisterMeetings(item, data);
            return Ok(result);
        }

        [HttpPost("InsertDetails")]
        public IActionResult InsertDetails(MeetingsDetailUpdateModel items)
        {

            var data = _mapper.Map<tbMeetingsDetails>(items);
            var result = _meetingsServices.RegisterMeetingsDetails(data);
            return Ok(result);
        }

        [HttpPut("Update/{Id}")]
        public IActionResult Update(MeetingsModel items, int Id)
        {
            var item = _mapper.Map<tbMeetings>(items);
            var data = _mapper.Map<List<tbMeetingsDetails>>(items.met_details);

            var result = _meetingsServices.UpdateMeetings(item, data, Id);
            return Ok(result);
        }

        [HttpDelete("Delete/{Id}")]
        public IActionResult Delete(int Id, int Mod)
        {
            var result = _meetingsServices.DeleteMeetings(Id, Mod);
            return Ok(result);
        }

        [HttpDelete("DeleteDetail/{Id}")]
        public IActionResult DeleteDetail(int Id, int Mod)
        {
            var result = _meetingsServices.DeleteDetail(Id, Mod);
            return Ok(result);
        }
    }
}
