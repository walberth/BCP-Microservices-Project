using Microsoft.EntityFrameworkCore;
using Pago.Domain.Models;
using Pago.Domain.Repositories;
using Pago.Infrastructure.Repositories.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pago.Infrastructure.Repositories
{
    public class PagoRepository : IPagoRepository
    {
        private readonly PagoDbContext _context;

        public PagoRepository(PagoDbContext context)
        {
            _context = context;
        }
        public async Task<Domain.Models.Pago> Registrar(Domain.Models.Pago pago)
        {
            try
            {
                _context.Add(pago);
                await _context.SaveChangesAsync();
                return pago;
            }
            catch (Exception ex)
            {
                return new Domain.Models.Pago();
            }
        }
        public async Task<bool> ActualizarProcesado(int idPago)
        {
            try
            {
                var affectedRows = await _context.Pagos.Where(x => x.IdPago.Equals(idPago))
                .ExecuteUpdateAsync(z => z.SetProperty(y => y.Procesado, 1));

                return affectedRows == 0 ? false : true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
