using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Models
{
    public class GlobalIdentitySettings
    {
        public string SIGNING_KEY { get; set; }
        public string ISSUER { get; set; }
        public string AUDIENCE { get; set; }
        public string IDENTITYDBCONN { get; set; }
    }
}
