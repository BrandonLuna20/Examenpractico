using Examenpractico.Domain.IRepositories;
using Examenpractico.Domain.Models;
using Examenpractico.Persistence.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examenpractico.Persistence.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AplicacionDbContext _context;
        public UsuarioRepository(AplicacionDbContext context)
        {
            _context = context;

        }

        public async Task SaveUser(Usuario usuario)
        {
            _context.Add(usuario);
            await _context.SaveChangesAsync();
        }

        public async Task<bool> ValidateExistence(Usuario usuario)
        {
            var validateExistence = await _context.Usuario.AnyAsync(x => x.NombreUsuario == usuario.NombreUsuario);
            return validateExistence;
        }

        public async Task<Usuario> ValidateContraseña(int idUsuario, string contraseñaAnterior)
        {
            var usuario = await _context.Usuario.Where(x => x.Id == idUsuario && x.Password == contraseñaAnterior).FirstOrDefaultAsync();
            return usuario;
        }

        public async Task UpdateContraseña(Usuario usuario)
        {
            _context.Update(usuario);
            await _context.SaveChangesAsync();
        }
    }
}
