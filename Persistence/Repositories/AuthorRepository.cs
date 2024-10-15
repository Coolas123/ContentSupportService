using Domain.Entities;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories
{
    public class AuthorRepository : BaseRepository<Author>, IAuthorRepository
    {
        public AuthorRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }

        public async Task<Author> GetByUrlPage(string urlPage) {
            return await dbSet.FirstOrDefaultAsync(x => x.UrlPage == urlPage);
        }

        public async Task<Author> GetByIdWithUserAndProfileMaterialsAsync(Guid id) {
            return await dbSet.Include(x=>x.User).ThenInclude(x=>x.ProfileMaterials).FirstOrDefaultAsync(x => x.UserId == id);
        }
    }
}
