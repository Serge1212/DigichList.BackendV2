using AutoMapper;
using DigichList.Backend.Interfaces;
using DigichList.Backend.ViewModel;
using DigichList.Core.Repositories;
using DigichList.TelegramNotifications.BotNotifications;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigichList.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DefectController : ControllerBase
    {
        private readonly IDefectService _service;
        private readonly IMapper _mapper;

        public DefectController(
            IDefectService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var defects = _service.GetAll();
            return Ok(_mapper.Map<IEnumerable<DefectViewModel>>(defects)); 
        }

        [HttpGet("getDefect")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var defect = await _service.GetByIdAsync(id);
            return defect != null ?
                Ok(_mapper.Map<DefectViewModel>(defect)) :
                NotFound($"Defect with id of {id} was not found");

        }

        [HttpPost]
        [Route("updateDefect")]
        public async Task<IActionResult> UpdateAsync([FromBody]DefectViewModel defectViewModel)
        {
            await _service.UpdateAsync(defectViewModel);
            return Ok();
        }

        [HttpDelete("deleteDefect")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            var result = await _service.DeleteAsync(id);
            
            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("deleteDefects")]
        public async Task<IActionResult> DeleteRangeAsync([FromQuery(Name = "idArr")] int[] idArr)
        {
            if(idArr.Length < 1)
            {
                return BadRequest("There wasn't any id provided to delete");
            }
            await _service.DeleteRangeAsync(idArr);
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> AssignAsync(int userId, int defectId)
        {
            var (isSuccess, message) = await _service.AssignAsync(userId, defectId);

            if (isSuccess)
            {
                return Ok();
            }

            return BadRequest(message);
        }
    }
}
