using System;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using apiMensaje.Entities;

namespace apiMensaje.Codigo
{
    public class capaAuth
    {
        public static async Task<Usuario> InicioSession(string pLogin, string pPassword, string pIp, string pHostName, string pUserAgent)
        {
            Usuario o = null;
            using (SqlServerContext context = new SqlServerContext())
            {
                o = await context.Usuarios.Where(x => x.Mail == pLogin && x.Pass == pPassword).FirstOrDefaultAsync();


            }
            return o;
        }
    }

}