﻿using AutoMapper;
using DigichList.Backend.Helpers;
using DigichList.Backend.ViewModel;
using DigichList.Core.Entities;
using DigichList.Core.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DigichList.Backend.Controllers
{
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IAdminRepository _repo;
        private readonly JwtService _jwtService;
        private readonly IMapper _mapper;

        public AdminController(IAdminRepository repo,
            JwtService jwtService,
            IMapper mapper)
        {
            _repo = repo;
            _jwtService = jwtService;
            _mapper = mapper;
        }
        [HttpGet]
        [Route("api/[controller]")]
        public async Task<IActionResult> GetAdmins()
        {
            var admins = await _repo.GetAllAsync();
            return Ok(_mapper.Map<IEnumerable<AdminViewModel>>(admins));
        }

        [HttpGet]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> GetAdmin(int id)
        {
            var admin = await _repo.GetByIdAsync(id);
            return admin != null ?
                Ok(_mapper.Map<AdminViewModel>(admin)) :
                NotFound($"Admin with id of {id} was not found");
        }

        [HttpPost]
        [Route("api/[controller]")]
        public async Task<IActionResult> CreateAdmin(Admin admin)
        {
            admin.Password = BCrypt.Net.BCrypt.HashPassword(admin.Password); 
            await _repo.AddAsync(admin);
            return CreatedAtAction("GetAdmin", new { id = admin.Id }, admin);
        }

        [HttpDelete]
        [Route("api/[controller]/{id}")]
        public async Task<IActionResult> DeleteAdmin(int id)
        {
            return await CommonControllerMethods
                .DeleteAsync<Admin, IAdminRepository>(id, _repo);
        }

        [HttpDelete("DeleteAdmins")]
        public async Task<IActionResult> DeleteAdmins([FromQuery(Name = "idArr")] int[] idArr)
        {
            if (idArr.Length < 1)
            {
                return NotFound("There wasn't any id provided to delete");
            }
            await _repo.DeleteRangeAsync(idArr);
            return Ok();
        }

        [HttpPost]
        [Route("api/[controller]/UpdateAdminPassword")]
        public async Task<IActionResult> UpdateAdminPassword([FromBody] ChangeAdminPasswordViewModel changePassword)
        {
            try
            {
                var admin = await _repo.GetAdminForChangePassword(changePassword.Id, changePassword.Password);
                admin.Password = BCrypt.Net.BCrypt.HashPassword(changePassword.Password);
                await _repo.UpdateAsync(admin);
                return Ok();
            }
            catch(ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("api/[controller]/UpdateAdmin")]
        public async Task<IActionResult> UpdateAdmin([FromBody] Admin admin)
        {
                var existingAdmin = await _repo.GetByIdAsync(admin.Id);
                existingAdmin.FirstName = admin.FirstName;
                existingAdmin.LastName = admin.LastName;
                existingAdmin.Email = admin.Email;
                existingAdmin.AccessLevel = admin.AccessLevel;
                return await CommonControllerMethods
                .UpdateAsync(existingAdmin, _repo);
        }

        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] LoginViewModel request)
        {
            var admin = await _repo.GetAdminByEmail(request.Email);

            if (admin == null)
                return BadRequest(new { message = "Invalid Credentials" });

            var passwordsMatch = BCrypt.Net.BCrypt.Verify(request.Password, admin.Password);
            if (!passwordsMatch)
            {
                return BadRequest(new { message = "Invalid Credentials" });
            }

            var jwt = _jwtService.Generate(admin.Id);

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

                var admin = await _repo.GetByIdAsync(adminId);

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