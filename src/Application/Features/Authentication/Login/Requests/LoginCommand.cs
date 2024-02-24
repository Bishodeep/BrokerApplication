using clean.Application.Common.Models.Authentication;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace clean.Application.Features.Authentication.Login.Requests
{
    public record LoginCommand(string UserName, string Password) : IRequest<LoginResponseDto>;
}
