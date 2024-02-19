using Microsoft.EntityFrameworkCore;
using Pago.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pago.Infrastructure.Repositories.Base
{
    public class PagoDbContext : DbContext
    {
        public PagoDbContext(DbContextOptions<PagoDbContext> options)
            : base(options)
        {

        }
        public virtual DbSet<Domain.Models.Pago> Pagos { get; set; }
        public virtual DbSet<Producto> Productos { get; set; }


        public virtual DbSet<Categoria> Categorias { get; set; }

        public virtual DbSet<Cliente> Clientes { get; set; }


        public virtual DbSet<Domain.Models.Venta> Ventas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(PagoDbContext).Assembly);
        }
    }
}
