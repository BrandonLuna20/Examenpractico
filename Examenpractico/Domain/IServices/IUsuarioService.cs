using Examenpractico.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examenpractico.Domain.IServices
{
    public interface IUsuarioService
    {
        Task SaveUser(Usuario usuario);
        Task<bool> ValidateExistence(Usuario usuario);
        Task<Usuario> ValidateContraseña(int idUsuario, string contraseñaAnterior);
        Task UpdateContraseña(Usuario usuario);
    }
}
