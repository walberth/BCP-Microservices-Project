using MongoDB.Bson.IO;
using MongoDB.Driver;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Venta.Application.Common;
using Venta.Domain.Models;
using Venta.Domain.Services.WebServices;

namespace Venta.Infrastructure.Services.WebServices
{
    public class PagoService : IPagoService
    {
        private readonly HttpClient _httpClientPago;
        public PagoService(HttpClient httpClientPago)
        {
            _httpClientPago = httpClientPago;
        }

        public async Task<bool> RealizarPago(Pago pago, int idVenta, decimal monto)
        {
            if (pago == null) { return false; }

            using var request = new HttpRequestMessage(HttpMethod.Post, "api/pago/realizarPago");

            var entidadSerializada = JsonSerializer.Serialize(new { IdVenta = idVenta, Monto = monto, FormaPago = pago.FormaPago, 
                NumeroTarjeta = pago.NumeroTarjeta, FechaVencimiento = pago.FechaVencimiento, CVV = pago.CVV,
                NombreTitular = pago.NombreTitular, NumeroCuotas = pago.NumeroCuotas
            });
            request.Content = new StringContent(entidadSerializada, Encoding.UTF8, MediaTypeNames.Application.Json);

            var response = await _httpClientPago.SendAsync(request);

            if(response.IsSuccessStatusCode)
            {
                var JsonString = await response.Content.ReadAsStringAsync();
                if(JsonString.ToUpper().Contains("TRUE"))
                {
                    return true;
                }
                else { return false; }
            }
            else { return false; }
        }
    }
}
