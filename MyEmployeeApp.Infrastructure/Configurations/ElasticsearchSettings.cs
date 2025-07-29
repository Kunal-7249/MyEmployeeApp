using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyEmployeeApp.Infrastructure.Configurations
{
   
    public class ElasticsearchSettings {
        public string Uri { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Index { get; set; }
    }

}
