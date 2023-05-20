using DigichList.Backend.Interfaces;
using DigichList.Backend.ViewModel;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DigichList.Backend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DefectController : ControllerBase
    {
        private readonly IDefectService _service;

        public DefectController(IDefectService service)
        {
            _service = service;
        }

        [HttpGet]
        public IActionResult Get()
        {
            var defects = _service.GetAll();
            return Ok(defects); 
        }

        [HttpGet("getDefect")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var defect = await _service.GetByIdAsync(id);
            return defect != null ?
                Ok(defect) :
                NotFound();
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

        [HttpPost("assignDefect")]
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
