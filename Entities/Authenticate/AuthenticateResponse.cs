using System;
using System.Collections.Generic;
using System.Text;

namespace apiMensaje.Entities.Authenticate
{
    public class AuthenticateResponse
    {
        public int UsuarioId { get; set; }
        public string Mail { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse()
        { }
        public AuthenticateResponse(Usuario user, string token)
        {
            UsuarioId = user.UsuarioId;
            Mail = user.Mail;
            Token = token;
            //Login = user.Login;
        }
    }
}
