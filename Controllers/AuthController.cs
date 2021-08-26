using System;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using apiMensaje.Business;
using apiMensaje.Entities;
using apiMensaje.Entities.Authenticate;
using System.Text;
using System.Security.Claims;

namespace apiMensaje.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private IUserService _userService;

        public AuthController(IUserService userService)
        {
            _userService = userService;
        }
        [HttpPost]
        public IActionResult Authenticate(AuthenticateRequest model)
        {
            var response = _userService.Authenticate(model);

            if (response == null)
                return BadRequest(new { message = "Nombre de usuario o contraseña incorrecta" });

            return Ok(response);
        }
    }
}