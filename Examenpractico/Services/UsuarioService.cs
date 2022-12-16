using Examenpractico.Domain.IRepositories;
using Examenpractico.Domain.IServices;
using Examenpractico.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examenpractico.Services
{
    public class UsuarioService: IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepositoty;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepositoty = usuarioRepository;
        }

        public async Task SaveUser(Usuario usuario)
        {
            await _usuarioRepositoty.SaveUser(usuario);
        }

        public async Task<bool> ValidateExistence(Usuario usuario)
        {
            return await _usuarioRepositoty.ValidateExistence(usuario);
        }

        public async Task<Usuario> ValidateContraseña(int idUsuario, string constraseñaAnterior)
        {
            return await _usuarioRepositoty.ValidateContraseña(idUsuario, constraseñaAnterior);
        }

        public async Task UpdateContraseña(Usuario usuario)
        {
            await _usuarioRepositoty.UpdateContraseña(usuario);
        }

    }
}
