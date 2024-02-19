﻿using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pago.Domain.Models;

namespace Pago.Infrastructure.Repositories.Base.EFConfigurations
{
    public class ProductoEntityTypeConfiguration
	: IEntityTypeConfiguration<Producto>
	{
		public void Configure(EntityTypeBuilder<Producto> builder)
		{
			builder.ToTable("Producto");
			builder.HasKey(c => c.IdProducto);
			builder.Property(c => c.PrecioUnitario).HasPrecision(18,2);

			builder.HasOne(p => p.Categoria).WithMany(p => p.Productos)
				.HasForeignKey(p => p.IdCategoria);
		}
	}
}
