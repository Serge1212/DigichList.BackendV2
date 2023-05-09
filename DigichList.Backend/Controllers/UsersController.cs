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
    public class UsersController : ControllerBase
    {
        private readonly IUserService _service;
        private readonly IMapper _mapper;

        public UsersController(IUserService service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public IActionResult GetAllAsync()
        {
            var users = _service.GetUsersWithRolesAsync();
            return Ok(_mapper.Map<IEnumerable<UserViewModel>>(users));
        }

        [HttpGet]
        [Route("api/[controller]/getRegisteredUsers")]
        public async Task<IActionResult> GetRegisteredUsersAsync()
        {
            var users = await _service.GetRegisteredUsersAsync();
            return Ok(_mapper.Map<IEnumerable<UserViewModel>>(users));
        }

        [HttpGet]
        [Route("api/[controller]/getUnregisteredUsers")]
        public async Task<IActionResult> GetUnregisteredUsersAsync()
        {
            var users = await _service.GetUnregisteredUsersAsync();
            return Ok(_mapper.Map<IEnumerable<UserViewModel>>(users));
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetAsync(int id)
        {
            var user = await _service.GetUserWithRoleAsync(id);
            return user != null ?
                Ok(_mapper.Map<UserViewModel>(user)) :
                NotFound();
        }

        [HttpGet]
        [Route("api/[controller]/getTechnicians")]
        public async Task<IActionResult> GetTechniciansAsync()
        {
            var technicians = await _service.GetTechniciansAsync();
            return technicians != null ?
                Ok(_mapper.Map<IEnumerable<TechnicianViewModel>>(technicians)) :
                NotFound();

        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> AddAsync(User user)
        {
            await _service.AddAsync(user);
            return CreatedAtAction("GetUser", new { id = user.Id }, user);
        }

        [HttpPost]
        [Route("api/[controller]/updateUser")]
        public async Task<IActionResult> UpdateAsync([FromBody] User user)
        {
            var result = await _service.UpdateAsync(user);

            if(result)
            {
                return Ok();
            }

            return BadRequest();
        }

        [HttpDelete]
        [Route("api/[controller]/deleteUser/{id}")]
        public async Task<IActionResult> DeleteOneAsync(int id)
        {
            var result = await _service.DeleteOneAsync(id);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpDelete("deleteUsers")]
        public async Task<IActionResult> DeleteManyAsync([FromQuery(Name = "idArr")] int[] idArr)
        {
            var result = await _service.DeleteManyAsync(idArr);

            if (result)
            {
                return Ok();
            }

            return NotFound();
        }
    }
}
