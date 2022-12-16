using Examenpractico.Domain.IServices;
using Examenpractico.Domain.Models;
using Examenpractico.DTO;
using Examenpractico.Utils;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Examenpractico.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly IUsuarioService _usuarioService;
        public UsuarioController(IUsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Usuario usuario)
        {
            try
            {
                var validateExistence = await _usuarioService.ValidateExistence(usuario);
                if (validateExistence)
                {
                    return BadRequest(new { message = "Este Usuario " + usuario.NombreUsuario + " Ya Existe!!" });
                }

                usuario.Password = Encriptar.EncriptacionPassword(usuario.Password);
                await _usuarioService.SaveUser(usuario);

                return Ok(new { message = "Usuario Registrado!!" });
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [Route("CambiarContraseña")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPut]
        public async Task<IActionResult> CambiarConstraseña([FromBody] CambiarContraseñaDTO cambiarContraseña)
        {
            try
            {
                var identity = HttpContext.User.Identity as ClaimsIdentity;

                int idUsuario = JwtConfigurator.GetTokenIdUsuario(identity);
                string contraseñaEncriptado = Encriptar.EncriptacionPassword(cambiarContraseña.contraseñaAnterior);
                var usuario = await _usuarioService.ValidateContraseña(idUsuario, contraseñaEncriptado);
                if (usuario == null)
                {
                    return BadRequest(new { message = "La Contraseña Es Incorrecta" });
                }
                else
                {
                    usuario.Password = Encriptar.EncriptacionPassword(cambiarContraseña.nuevaContraseña);
                    await _usuarioService.UpdateContraseña(usuario);
                    return Ok(new { message = "La Contraseña Fue Actualizada" });
                }

            }

            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
