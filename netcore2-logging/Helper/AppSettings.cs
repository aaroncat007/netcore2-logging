using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace netcore2_logging.Helper
{
    public class JWTSettings
    {
        public string Secret { set; get; }

        public string Issuer { set; get; }
    }
}
