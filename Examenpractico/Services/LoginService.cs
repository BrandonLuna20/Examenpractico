using Examenpractico.Domain.IRepositories;
using Examenpractico.Domain.IServices;
using Examenpractico.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examenpractico.Services
{
    public class LoginService: ILoginService
    {
        public readonly ILoginRepository _LoginRepository;

        public LoginService(ILoginRepository loginRepository)
        {
            _LoginRepository = loginRepository;
        }

        public async Task<Usuario> ValidateUser(Usuario usuario)
        {
            return await _LoginRepository.ValidateUser(usuario);
        }
    }
}
