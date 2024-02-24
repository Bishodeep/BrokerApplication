using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clean.Infrastructure.Models
{
    public class JwtIssuerOptions
    {
        public string Secret { get; set; }
        public string Issuer { get; set; }
        public int ExpireDays { get; set; }
    }
}
