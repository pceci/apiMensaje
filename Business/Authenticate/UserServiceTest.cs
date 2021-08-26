using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using apiMensaje.Entities.Authenticate;
using apiMensaje.Helpers;
using apiMensaje.Entities;

namespace apiMensaje.Business
{
    public class UserServiceTest : IUserService
    {
        // users hardcoded for simplicity, store in a db with hashed passwords in production applications
        private List<Usuario> _users = new List<Usuario>
        {
            new Usuario { UsuarioId = 1, Mail = "test"}
        };

        private readonly AppSettings _appSettings;

        public UserServiceTest(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            var user = _users.SingleOrDefault(x => x.Mail == model.login);

            // return null if user not found
            if (user == null) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(user);

            return new AuthenticateResponse(user, token);
        }

        public IEnumerable<Usuario> GetAll()
        {
            return _users;
        }

        public Usuario GetById(int UsuarioId)
        {
            return _users.FirstOrDefault(x => x.UsuarioId == UsuarioId);
        }

        // helper methods

        private string generateJwtToken(Usuario user)
        {
            // generate token that is valid for 7 days
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("UsuarioId", user.UsuarioId.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
