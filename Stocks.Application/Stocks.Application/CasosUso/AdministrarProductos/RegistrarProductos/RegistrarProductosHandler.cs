using AutoMapper;
using MediatR;
using Stocks.Application.Common;
using Stocks.Domain.Models;
using Stocks.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Stocks.Application.CasosUso.AdministrarProductos.RegistrarProductos
{
    public class RegistrarProductoHandler :
            IRequestHandler<RegistrarProductosRequest, IResult>
    {
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;

        public RegistrarProductoHandler(IProductoRepository productoRepository, IMapper mapper)
        {
            _productoRepository = productoRepository;
            _mapper = mapper;
        }


        public async Task<IResult> Handle(RegistrarProductosRequest request, CancellationToken cancellationToken)
        {
            IResult response = null;

            try
            {

                response = (await _productoRepository.Adicionar(_mapper.Map<Producto>(request)) == true) ? new SuccessResult() : new FailureResult();

                return response;
            }
            catch (Exception ex)
            {
                response = new FailureResult();
                return response;
            }
        }
    }
}
