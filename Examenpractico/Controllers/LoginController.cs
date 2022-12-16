using Examenpractico.Domain.IServices;
using Examenpractico.Domain.Models;
using Examenpractico.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examenpractico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ILoginService _LoginService;
        private readonly IConfiguration _config;
        public LoginController(ILoginService loginService, IConfiguration config)
        {
            _LoginService = loginService;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Usuario usuario)
        {
            try
            {
                usuario.Password = Encriptar.EncriptacionPassword(usuario.Password);
                var user = await _LoginService.ValidateUser(usuario);
                if(user == null)
                {
                    return BadRequest(new { message = "El Usuario o la Contraseña es Invalida" });
                }
                string tokenString = JwtConfigurator.GetToken(user, _config);

                return Ok(new { token = tokenString });
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
