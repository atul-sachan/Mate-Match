using System.Threading.Tasks;
using Mate_Match_API.Data.Models;

namespace Mate_Match_API.Data.Interfaces
{
    public interface IAuthRepository
    {
         Task<User> Register(User user,string password);
         Task<User> Login(string username,string password);
         Task<bool> IsExists(string username);
    }
}