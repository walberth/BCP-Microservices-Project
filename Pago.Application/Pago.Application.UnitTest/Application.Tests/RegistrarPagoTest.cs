using AutoMapper;
using NSubstitute;
using Pago.Application.CasosUso.RegistrarPago;
using Pago.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Pago.Application.UnitTest.Application.Tests
{
    public class RegistrarPagoTest
    {
        private readonly IPagoRepository _pagoRepository;
        private readonly IMapper _mapper;
        private readonly RegistrarPagoHandler _registrarPagoHandler;

        public RegistrarPagoTest()
        {
            _pagoRepository = Substitute.For<IPagoRepository>();
            _mapper = new MapperConfiguration(c => c.AddProfile<RegistrarPagoMapper>()).CreateMapper();
            _registrarPagoHandler = Substitute.For<RegistrarPagoHandler>(_pagoRepository, _mapper);
        }

        [Fact]
        public async Task RealizarPago()
        {
            CancellationToken cancellationToken = new();
            RegistrarPagoRequest request = new();
            var response = await _registrarPagoHandler.Handle(request, cancellationToken);
            Assert.True(response.HasSucceeded);
        }
    }
}
