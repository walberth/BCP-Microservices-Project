using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pago.Application.CasosUso.RegistrarPago
{
    public class RegistrarPagoValidator : AbstractValidator<RegistrarPagoRequest>
    {
        public RegistrarPagoValidator()
        {
            RuleFor(item => item.IdVenta).NotNull().NotEmpty().GreaterThanOrEqualTo(0).WithMessage("IdVenta nulo, vacío o menor a 0");
            RuleFor(item => item.Monto).NotNull().NotEmpty().GreaterThan(0).WithMessage("Monto nulo, vacío o menor o igual a 0");
            RuleFor(item => item.FormaPago).NotNull().NotEmpty().GreaterThan(0).LessThan(4).WithMessage("FormaPago nulo, vacío o diferente a 1, 2 o 3");

            RuleFor(item => item.NumeroTarjeta).NotNull().NotEmpty().Length(16,16).When(x => (x.FormaPago == 1 || x.FormaPago == 2)).WithMessage("Número de tarjeta debe contener 16 dígitos");
            RuleFor(item => item.FechaVencimiento).NotNull().NotEmpty().GreaterThan(DateTime.Now).When(x => (x.FormaPago == 1 || x.FormaPago == 2)).WithMessage("Fecha de Vencimiento de la tarjeta debe ser mayor a la fecha actual");
            RuleFor(item => item.CVV).NotNull().NotEmpty().Length(3, 3).When(x => (x.FormaPago == 1 || x.FormaPago == 2)).WithMessage("CVV debe contener 3 dígitos");
            RuleFor(item => item.NombreTitular).NotNull().NotEmpty().Length(1, 100).When(x => (x.FormaPago == 1 || x.FormaPago == 2)).WithMessage("Ingrese un nombre de titular válido");
            RuleFor(item => item.NumeroCuotas).NotNull().NotEmpty().GreaterThanOrEqualTo(1).When(x => (x.FormaPago == 1 || x.FormaPago == 2)).WithMessage("Número de cuotas menor a 1");
        }
    }
}
