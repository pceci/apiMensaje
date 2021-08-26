using System;
using System.Collections.Generic;
using Microsoft.Extensions.Options;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Threading.Tasks;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using apiMensaje.Entities.Authenticate;
using apiMensaje.Entities;
using apiMensaje.Helpers;


namespace apiMensaje.Business
{
    public class UserService : IUserService
    {
        private readonly AppSettings _appSettings;

        public UserService(IOptions<AppSettings> appSettings)
        {
            _appSettings = appSettings.Value;
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            //string userAgent = System.Web.HttpContext.Current.Request.UserAgent;
            //string ip = System.Web.HttpContext.Current.Server.HtmlEncode(System.Web.HttpContext.Current.Request.UserHostAddress);
            //string hostName = System.Web.HttpContext.Current.Request.UserHostName;
            if (string.IsNullOrEmpty(model.login) || string.IsNullOrEmpty(model.pass)) return null;
            //DataTable dt = DKbase.baseDatos.StoredProcedure.Login(model.login, model.pass, "", "", "");

            // Usuario o = Codigo.capaAuth.InicioSession(model.login, model.pass, "", "", "");
            var t = Task.Run(() => Codigo.capaAuth.InicioSession(model.login, model.pass, "", "", ""));
            t.Wait();
            if (t.Result == null)
            {
                //throw new Exception("Result == null");
                return null;
            }
            Usuario o = t.Result;

            if (o == null || o.UsuarioId == -1) return null;

            // authentication successful so generate jwt token
            var token = generateJwtToken(o);

            return new AuthenticateResponse(o, token);

        }
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