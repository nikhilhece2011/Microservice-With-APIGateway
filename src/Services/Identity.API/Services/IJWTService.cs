using Identity.API.Infrastructure.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Identity.API.Services
{
    public interface IJWTService
    {
        object GetAccessToken(Users loginResult);
    }
}
