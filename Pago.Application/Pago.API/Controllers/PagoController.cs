using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Pago.Application.CasosUso.RegistrarPago;

namespace Pago.Api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class PagoController : ControllerBase
    {
        private readonly IMediator _mediator;
        public PagoController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("realizarPago")]
        public async Task<IActionResult> Registrar([FromBody] RegistrarPagoRequest request)
        {
            var response = await _mediator.Send(request);
            return Ok(response);
        }
    }
}
