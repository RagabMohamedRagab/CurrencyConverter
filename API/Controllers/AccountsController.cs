using BLL.Dtos;
using BOL;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountsController : ControllerBase
    {
        private readonly IRegisterAndLoginServices _registerAndLogin;

        public AccountsController(IRegisterAndLoginServices registerAndLogin)
        {
            _registerAndLogin = registerAndLogin;
        }
        // api/Accounts/Register
        [HttpPost("Register")]
        public IActionResult Register([FromForm] RegisterDTO dTO)
        {
            if (_registerAndLogin.CreateAsync(dTO).Result > 0)
            {
                return Ok("Successfully Register");
            }
            return NotFound("Falied Register");
        }
        // api/Accounts/Login
        [HttpPost("Login")]
        public IActionResult Login([FromForm] LoginDto dto)
        {
            if (_registerAndLogin.LoginAsync(dto).Result > 0)
            {
                return Ok("Successfully Login");
            }
            return NotFound("Falied Login");
        }













    }
}
