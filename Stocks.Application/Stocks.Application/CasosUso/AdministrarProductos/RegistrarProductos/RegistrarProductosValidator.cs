﻿using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Application.CasosUso.AdministrarProductos.RegistrarProductos
{
    public class RegistrarProductosValidator : AbstractValidator<RegistrarProductosRequest>
    {
        public RegistrarProductosValidator()
        {

            RuleFor(item => item.Nombre).NotNull().NotEmpty().Length(1, 50).WithMessage("Nombre nulo, vacío o con más de 50 caracteres");
            RuleFor(item => item.Stock).NotNull().GreaterThanOrEqualTo(0).WithMessage("Stock nulo o menor a 0");
            RuleFor(item => item.StockMinimo).NotNull().GreaterThanOrEqualTo(0).WithMessage("StockMinimo nulo o menor a 0");
            RuleFor(item => item.PrecioUnitario).NotNull().GreaterThanOrEqualTo(0).WithMessage("PrecioUnitario nulo o menor a 0");
        }
    }
}
