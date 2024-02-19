using AutoMapper;
using Stocks.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Application.CasosUso.AdministrarProductos.RegistrarProductos
{
    public class RegistrarProductoMapper : Profile
    {
        public RegistrarProductoMapper()
        {
            CreateMap<RegistrarProductosRequest, Producto>();
        }
    }
}
