using Business.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Sys;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Business.Conctrate
{
    public class UserService : IUserService
    {
        private IUnitofWork _unitofWork;
        public UserService(IUnitofWork unitofWork)
        {
            _unitofWork = unitofWork;
        }

        public User GenerateToken(User user)
        {
            try
            {


                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes("Is*K|SNH.~!k'wwVgPi'pNTY-[},^N<xTOpxmSE+M4JUb]5)dVifRif|KovVuwA");

                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(new[] {
                    new Claim("id", user.Id.ToString()),
                    new Claim(ClaimTypes.NameIdentifier, user.Name),

                }),
                    Expires = DateTime.UtcNow.AddHours(1),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),

                };

        user.Roles.ForEach(x => tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Role, x.Role.Name)));

                var token = tokenHandler.CreateToken(tokenDescriptor);
                user.Token = tokenHandler.WriteToken(token);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return user;
        }


        public IQueryable<User> GetAllUser() => _unitofWork.UserRepository.GetAll().Include(x => x.Roles).ThenInclude(x => x.Role);
        public async Task<User> GetUser(long Id) => await _unitofWork.UserRepository.Find(x => x.Id == Id).Include(x => x.Roles).ThenInclude(x => x.Role).FirstOrDefaultAsync();
        public async Task<List<UserRole>> GetUserRole(long Id) => await _unitofWork.UserRoleRepository.Find(x => x.Id == Id).Include(x => x.Role).ToListAsync();

        public async Task<User> Authenticate(string username, string password)
        {
            var user = await _unitofWork.UserRepository.Find(x => x.Name == username && x.PasswordHash == password).Include(x=> x.Roles).ThenInclude(x=> x.Role).FirstOrDefaultAsync();
            user = GenerateToken(user);
            return user;
        }
    }
}
