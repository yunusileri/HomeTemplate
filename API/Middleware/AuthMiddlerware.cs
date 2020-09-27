using Business.Abstract;
using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Middleware
{
    public class AuthMiddlerware
    {
        private readonly RequestDelegate _next;
        HttpContext _context;
        private IUserService _userService;
        public AuthMiddlerware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
            var token = _context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();

            if (token != null)
            {
                await attachUserToContext(token);
            }
            await _next(_context);
        }
        private async Task attachUserToContext(string token)
        {

            try
            {

                var tokenHandler = new JwtSecurityTokenHandler();

                var key = Encoding.ASCII.GetBytes("Is*K|SNH.~!k'wwVgPi'pNTY-[},^N<xTOpxmSE+M4JUb]5)dVifRif|KovVuwA");
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,

                    ClockSkew = TimeSpan.Zero
                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                long userId = long.Parse(jwtToken.Claims.First(x => x.Type == "id").Value);
                var _users = await _userService.GetUser(userId);

                _context.Items["User"] = _users;
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex);
            }
        }
    }
}
