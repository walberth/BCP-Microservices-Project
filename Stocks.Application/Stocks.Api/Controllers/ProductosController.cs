using MediatR;
using Microsoft.AspNetCore.Mvc;
using Stocks.Application.CasosUso.AdministrarProductos.ConsultarProductos;
using Stocks.Application.CasosUso.AdministrarProductos.RegistrarProductos;

namespace Stocks.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class ProductosController : ControllerBase
    {
        private readonly IMediator _mediator;
        public ProductosController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("consultar")]
        public async Task<IActionResult> Consultar([FromQuery] ConsultarProductosRequest request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
        [HttpPost("registrar")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarProductosRequest request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
        [HttpPut("reservar")]
        public async Task<IActionResult> Reservar([FromBody] ReservarStockRequest request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }

        [HttpPut("reponer")]
        public async Task<IActionResult> Reponer([FromBody] ReponerStockRequest request)
        {
            var response = await _mediator.Send(request);

            return Ok(response);
        }
    }
}
