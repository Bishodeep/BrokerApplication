using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clean.Application.Common.Models.Authentication
{
    public class LoginResponseDto
    {
        public string Token { get; set; }
        public string UserName { get; set; }
    }
}
