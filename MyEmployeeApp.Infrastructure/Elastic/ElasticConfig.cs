using Elasticsearch.Net;
using MyEmployeeApp.Infrastructure.Configurations;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEmployeeApp.Infrastructure.Elastic
{
    public static class ElasticConfig
    {
        public static IElasticClient CreateClient(ElasticsearchSettings settings)
        {
            var connectionSettings = new ConnectionSettings(new Uri(settings.Uri))
                .BasicAuthentication(settings.Username, settings.Password)
                .ServerCertificateValidationCallback(CertificateValidations.AllowAll)
                .DefaultIndex(settings.Index)
                .EnableDebugMode();

            return new ElasticClient(connectionSettings);
        }
    }
}
