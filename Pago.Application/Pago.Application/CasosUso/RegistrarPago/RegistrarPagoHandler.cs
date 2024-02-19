using AutoMapper;
using MediatR;
using Pago.Application.Common;
using Pago.Domain.Models;
using Pago.Domain.Repositories;
using Pago.Domain.Services.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pago.Application.CasosUso.RegistrarPago
{
    public class RegistrarPagoHandler :
            IRequestHandler<RegistrarPagoRequest, IResult>
    {
        private readonly IPagoRepository _pagoRepository;
        private readonly IMapper _mapper;
        private readonly IBancoService _bancoService;

        public RegistrarPagoHandler(IPagoRepository pagoRepository, IMapper mapper, IBancoService bancoService)
        {
            _pagoRepository = pagoRepository;
            _mapper = mapper;
            _bancoService = bancoService;
        }

        public async Task<IResult> Handle(RegistrarPagoRequest request, CancellationToken cancellationToken)
        {
            IResult response = null;

            try
            {
                Domain.Models.Pago pago = await _pagoRepository.Registrar(_mapper.Map<Domain.Models.Pago>(request));
                if(pago != null && pago.IdPago > 0 && pago.FormaPago != 3) {
                    response = (await _bancoService.ProcesarPago(pago) == true) ? new SuccessResult(): new FailureResult();
                    if(response.HasSucceeded)
                    {
                        await _pagoRepository.ActualizarProcesado(pago.IdPago);
                    }
                    else
                    {
                       response = new FailureResult();
                    }
                }
                else
                {
                    response = new FailureResult();
                }
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
