using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Identity.API.Dtos;
using Identity.API.Infrastructure;
using Identity.API.Models;
using Identity.API.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly IdentityDbContext _context;
        private readonly IJWTService _jwtService;
        
        public TokenController(IdentityDbContext context, 
            IJWTService jwtService)
        {
            _context = context;
            _jwtService = jwtService;
        }

        [HttpPost]
        public IActionResult Post([FromBody]LoginDto model)
        {
            var loginResult = _context.Users.FirstOrDefault(m => m.UserName == model.UserName && m.Password == model.Password);
            if (loginResult != null)
            {
                return Ok(_jwtService.GetAccessToken(loginResult));
            }
            else
            {
                return Unauthorized();
            }
        }

        [Authorize]
        [HttpGet]

        public string Get()
        {
            return "hi from Get";
        }
    }
}