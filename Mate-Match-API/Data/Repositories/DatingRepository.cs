using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Mate_Match_API.Data.Interfaces;
using Mate_Match_API.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace Mate_Match_API.Data.Repositories
{
    public class DatingRepository : IDatingRepository
    {
        private readonly DataContext _context;
        public DatingRepository(DataContext context)
        {
            _context = context;

        }
        public void Add<T>(T entity) where T : class
        {
            _context.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _context.Remove(entity);
        }

        public async Task<Photo> GetMainPhotoForUser(int userId)
        {
            return await _context.Photos.Where(u => u.UserId == userId)
                .FirstOrDefaultAsync(p => p.IsMain);
        }

        public async Task<User> GetUser(int userId)
        {
            var user = await _context.Users.Include(p=>p.Photos).FirstOrDefaultAsync(u=> u.Id == userId);
            return user;
        }

        public async Task<Photo> GetPhoto(int id){
            var photo =await _context.Photos.FirstOrDefaultAsync(p=>p.Id == id);
            return photo;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _context.Users.Include(P=>P.Photos).ToListAsync();
            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await _context.SaveChangesAsync() > 0;
        }
    }
}