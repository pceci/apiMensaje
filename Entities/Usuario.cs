using System;
using System.Collections.Generic;

namespace apiMensaje.Entities
{
    public class Usuario
    {
        public int UsuarioId { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Mail { get; set; }
        public string Pass { get; set; }
        //public string Login { get; set; }
        public List<Nota> Notas { get; } = new List<Nota>();
    }
}