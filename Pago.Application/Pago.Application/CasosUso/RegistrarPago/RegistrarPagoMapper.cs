using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pago.Application.CasosUso.RegistrarPago
{
    public class RegistrarPagoMapper : Profile
    {
        public RegistrarPagoMapper()
        {
            CreateMap<RegistrarPagoRequest, Domain.Models.Pago>();
        }
    }
}
