using System;
using System.Linq;
using System.Threading.Tasks;
using Mate_Match_API.Data.Interfaces;
using Mate_Match_API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Mate_Match_API.Data.Repositories
{
    public class AuthRepository : IAuthRepository
    {
        private readonly DataContext context;
        public AuthRepository(DataContext context)
        {
            this.context = context;

        }
        public async Task<bool> IsExists(string username)
        {
            if(await this.context.Users.AnyAsync(x=>x.Username == username))
                return true;
            
            return false;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = await this.context.Users.Include(p=>p.Photos).FirstOrDefaultAsync(x=>x.Username == username);
            if(user == null){
                return null;
            }
            if(!VerifyPasswordHash(password,user.PasswordHash,user.PasswordSalt)){
                return null;
            }

            return user;
        }

        private bool VerifyPasswordHash(string password, byte[] passwordHash, byte[] passwordSalt)
        {
            using(var Hmac = new System.Security.Cryptography.HMACSHA512(passwordSalt)){
                var computedHash = Hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if(computedHash[i]!= passwordHash[i]) return false;
                }
            }
            return true;
        }

        public async Task<User> Register(User user, string password)
        {
            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password,out passwordHash,out passwordSalt);
            user.PasswordSalt = passwordSalt;
            user.PasswordHash = passwordHash;

            await this.context.Users.AddAsync(user);
            await this.context.SaveChangesAsync();

            return user;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var Hmac = new System.Security.Cryptography.HMACSHA512()){
                passwordSalt = Hmac.Key;
                passwordHash = Hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }
    }
}