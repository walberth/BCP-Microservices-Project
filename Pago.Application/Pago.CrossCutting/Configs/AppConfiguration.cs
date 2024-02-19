using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pago.CrossCutting.Configs
{
    public class AppConfiguration
    {
        private readonly IConfiguration _configInfo;
        public AppConfiguration(IConfiguration configInfo)
        {
            _configInfo = configInfo;
        }

        public string ConexionDBPago
        {
            get
            {
                return _configInfo["dbPago-cnx"];
            }
            private set { }
        }
        public string LogMongoServerDB
        {
            get
            {
                return _configInfo["log-mongo-server-db"];
            }
            private set { }
        }

        public string LogMongoDbCollection
        {
            get
            {
                return _configInfo["log-mongo-db-collection"];
            }
            private set { }
        }
        public string UrlBaseServicioBanco
        {
            get
            {
                return _configInfo["url-base-servicio-banco"];
            }
            private set { }
        }
    }
}
