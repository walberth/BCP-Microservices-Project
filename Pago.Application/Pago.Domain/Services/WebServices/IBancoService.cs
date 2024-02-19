using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Pago.Domain.Models;

namespace Pago.Domain.Services.WebServices
{
    public interface IBancoService
    {
        Task<bool> ProcesarPago(Domain.Models.Pago pago);
    }
}
