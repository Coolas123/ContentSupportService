using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;

namespace Persistence.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public async Task<User> GetByEmailAsync(string email) {
            return await dbSet.FirstOrDefaultAsync(x => x.Email == email);
        }

        public async Task<User> GetByIdWithMaterialsAsync(Guid id) {
            return await dbSet.Include(x=>x.ProfileMaterials).FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
