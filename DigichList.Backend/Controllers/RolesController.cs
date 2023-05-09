using AutoMapper;
using DigichList.Backend.Interfaces;
using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigichList.Backend.Controllers
{
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleService _service;
        private readonly IMapper _mapper;

        public RolesController(
            IRoleService service,
            IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetAllAsync()
        {
            var roles = await _service.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<RoleViewModel>>(roles));
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            var role = await _service.GetByIdAsync(id);
            return role != null ?
                Ok(_mapper.Map<ExtendedRoleViewModel>(role)) :
                NotFound();
        }

        [HttpPost]
        [Route("api/[controller]/updateRole")]
        public async Task<IActionResult> UpdateAsync([FromBody] Role role)
        {
            await _service.UpdateAsync(role);
            return Ok();
        }

        [HttpPost]
        [Route("api/[controller]/assignRole")]
        public async Task<IActionResult> AssignAsync(int userId, int roleId)
        {
            var (isSuccess, message) = await _service.AssignAsync(userId, roleId);

            if(isSuccess)
            {
                return Ok();
            }

            return BadRequest(message);
        }

        [HttpPost]
        [Route("api/[controller]/removeRoleFromUser")]
        public async Task<IActionResult> RemoveRoleFromUserAsync(int userId)
        {
            var (isSuccess, message) = await _service.RemoveRoleFromUserAsync(userId);
            
            if (isSuccess)
            {
                return Ok();
            }

            return BadRequest(message);
        }

    }
}
