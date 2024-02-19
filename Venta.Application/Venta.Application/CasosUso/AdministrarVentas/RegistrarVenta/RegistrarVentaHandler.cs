using AutoMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Venta.Application.Common;
using Venta.Domain.Models;
using Venta.Domain.Repositories;
using Venta.Domain.Services.WebServices;
using Models = Venta.Domain.Models;

namespace Venta.Application.CasosUso.AdministrarVentas.RegistrarVenta
{
    public class RegistrarVentaHandler :
        IRequestHandler<RegistrarVentaRequest, IResult>
    {
        private readonly IVentaRepository _ventaRepository;
        private readonly IProductoRepository _productoRepository;
        private readonly IMapper _mapper;
        private readonly IStocksService _stocksService;
        private readonly IPagoService _pagoService;
        private readonly ILogger _logger;

        public RegistrarVentaHandler(IVentaRepository ventaRepository, IProductoRepository productoRepository, IMapper mapper,
            IStocksService stocksService, ILogger<RegistrarVentaHandler> logger, IPagoService pagoService)
        {
            _ventaRepository = ventaRepository;
            _productoRepository = productoRepository;
            _mapper = mapper;
            _stocksService = stocksService;
            _pagoService = pagoService;
            _logger = logger;
        }

        public async Task<IResult> Handle(RegistrarVentaRequest request, CancellationToken cancellationToken)
        {
            IResult response = null;
            try
            { 
                var venta = _mapper.Map<Models.Venta>(request);
                _logger.LogInformation($"Cantidad de productos: {venta.Detalle.Count()}");

                foreach (var detalle in venta.Detalle)
                {
                    var productoEncontrado = await _productoRepository.Consultar(detalle.IdProducto);
                    if (productoEncontrado == null || productoEncontrado?.IdProducto <= 0)
                    {
                        throw new Exception($"Producto no encontrado, código {detalle.IdProducto}");
                    }
                    if(productoEncontrado.Stock < detalle.Cantidad)
                    {
                        throw new Exception($"Producto sin stock, código {detalle.IdProducto}");
                    }
                    detalle.Precio = productoEncontrado.PrecioUnitario;   
                }
                foreach (var detalle in venta.Detalle)
                {
                    bool ok = await _stocksService.ActualizarStock(detalle.IdProducto, detalle.Cantidad) == true ? 
                        (await _productoRepository.ModificarStock(detalle.IdProducto, detalle.Cantidad) == true) ? 
                        true : throw new Exception($"Error actualizando stock (SQL), código {detalle.IdProducto}") :
                        throw new Exception($"Error actualizando stock (Mongo DB), código {detalle.IdProducto}");
                }
                response = await _ventaRepository.Registrar(venta) == true ? 
                    (await _pagoService.RealizarPago(_mapper.Map<Pago>(request.Pago), venta.IdVenta, venta.Monto) == true ? 
                    new SuccessResult() : new FailureResult()) : new FailureResult();

                return response;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, ex.Message);
                response = new FailureResult();
                return response;
            }
        }
    }
}
