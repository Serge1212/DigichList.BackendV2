using DigichList.Backend.Helpers;
using DigichList.Backend.Interfaces;
using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace DigichList.Backend.Controllers
{
    [ApiController]
    public class AdminController : ControllerBase
    {
        readonly IAdminService _service;
        readonly JwtService _jwtService;

        public AdminController(IAdminService service,
            JwtService jwtService)
        {
            _service = service;
            _jwtService = jwtService;
        }

        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetAdmins()
        {
            var admins = await _service.GetAllAsync();
            return Ok(admins);
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetAdmin(int id)
        {
            var admin = await _service.GetByIdAsync(id);
            return admin != null ? Ok(admin) : NotFound();
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> CreateAdmin(Admin admin)
        {
            await _service.AddAsync(admin);
            return CreatedAtAction("GetAdmin", new { id = admin.Id }, admin);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            await _service.DeleteAsync(id);
            return Ok();
        }

        [HttpDelete("deleteAdmins")]
        public async Task<IActionResult> DeleteAdmins([FromQuery(Name = "idArr")] int[] idArr)
        {
            if (idArr.Length < 1)
            {
                return BadRequest("There wasn't any id provided to delete.");
            }
            await _service.DeleteRangeAsync(idArr);
            return Ok();
        }

        [HttpPost]
        [Route("api/[controller]/updateAdminPassword")]
        public async Task<IActionResult> UpdateAdminPasswordAsync([FromBody] ChangeAdminPasswordViewModel changePassword)
        {
            var result = await _service.UpdatePasswordAsync(changePassword);

            if(result)
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPost]
        [Route("api/[controller]/UpdateAdmin")]
        public async Task<IActionResult> UpdateAdminAsync([FromBody] Admin admin)
        {
            await _service.UpdateAdminAsync(admin);
            return Ok();
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel request)
        {
            (bool result, int adminId) = await _service.VerifyAdminAsync(request);

            if(!result)
            {
                return BadRequest("Invalid Credentials");
            }

            var jwt = _jwtService.Generate(adminId);

            Response.Cookies.Append("jwt", jwt, new CookieOptions
            {
                HttpOnly = true,
                SameSite = SameSiteMode.None,
                Secure = true,
            });

            return Ok(new 
            { 
                message = "Success"
            });
        }

        [HttpGet("admin")]
        public async Task<IActionResult> Admin()
        {
            try
            {
                var jwt = Request.Cookies["jwt"];

                var token = _jwtService.Verify(jwt);

                int adminId = int.Parse(token.Issuer);

                var admin = await _service.GetByIdAsync(adminId);

                return Ok(admin);
            }
            catch(Exception ex)
            {
                return Unauthorized(ex.Message);
            }
        }

        [HttpPost("Logout")]
        public IActionResult Logout()
        {
            CookieOptions options = new()
            {
                Expires = DateTime.Today.AddDays(1),
                Secure = true,
                SameSite = SameSiteMode.None,
                HttpOnly = true
            };

            Response.Cookies.Delete("jwt", options);
            

            return Ok(new
            {
                message = "success"
            });
        }
    }
}