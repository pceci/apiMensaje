using System;

namespace apiMensaje.Entities
{
    public class Nota
    {
        public int NotaId { get; set; }
        public string Título { get; set; }
        public string Contenido { get; set; }
        public DateTime fechaCreacion { get; set; }
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }
    }
}