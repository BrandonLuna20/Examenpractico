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
    public class LoginRepository: ILoginRepository
    {
        private readonly AplicacionDbContext _context;
        public LoginRepository(AplicacionDbContext context)
        {
            _context = context;
        }

        public async Task<Usuario> ValidateUser(Usuario usuario)
        {
            var user = await _context.Usuario.Where(x => x.NombreUsuario == usuario.NombreUsuario && x.Password == usuario.Password).FirstOrDefaultAsync();
            return user;
        }
    }
}
