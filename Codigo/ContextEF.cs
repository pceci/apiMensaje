using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using apiMensaje.Entities;

namespace apiMensaje.Codigo
{
    public interface IapiContext
    {
        DbSet<Usuario> Usuarios
        {
            get;
            set;
        }
        DbSet<Nota> Notas
        {
            get;
            set;
        }
        DbContext context
        {
            get;
        }
    }
    public class SqlServerContext : DbContext, IapiContext
    {
        public IConfiguration Configuration { get; }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Nota> Notas { get; set; }
        public DbContext context
        {
            get => this;
        }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
         => options.UseSqlServer(Codigo.Helper.getConnectionStringSQL);
    }


}