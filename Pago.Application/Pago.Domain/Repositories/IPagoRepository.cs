using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pago.Domain.Repositories
{
    public interface IPagoRepository:IRepository
    {
        Task<Domain.Models.Pago> Registrar(Pago.Domain.Models.Pago entity);
        Task<bool> ActualizarProcesado(int idPago);
    }
}
