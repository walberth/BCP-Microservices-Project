using Azure.Core;
using Azure;
using Pago.Domain.Services.WebServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;


namespace Pago.Infrastructure.Services.WebServices
{
    public class BancoService : IBancoService
    {
        private readonly HttpClient _httpClientStocks;
        public BancoService(HttpClient httpClientStocks)
        {
            _httpClientStocks = httpClientStocks;
        }

        public async Task<bool> ProcesarPago(Domain.Models.Pago pago)
        {
            Random rnd = new Random();
            int numeroRandom = rnd.Next(56);
            bool response = (numeroRandom <= 50) ? true : false;
            return response;
        }
    }
}
