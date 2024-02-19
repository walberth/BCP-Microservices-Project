using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pago.Domain.Models;

namespace Pago.Infrastructure.Repositories.Base.EFConfigurations
{
    public class PagoEntityTypeConfiguration
    : IEntityTypeConfiguration<Domain.Models.Pago>
    {
        public void Configure(EntityTypeBuilder<Domain.Models.Pago> builder)
        {
            builder.ToTable("Pago");
            builder.HasKey(c => c.IdPago);
            builder.Property(c => c.Monto).HasPrecision(18, 2);

            builder.HasOne(p => p.Venta).WithMany(p => p.Pagos)
                .HasForeignKey(p => p.IdVenta);
        }
    }
}
