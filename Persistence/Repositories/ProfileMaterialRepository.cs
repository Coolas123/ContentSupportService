using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
    public sealed class ProfileMaterialRepository : BaseRepository<ProfileMaterial>, IProfileMaterialRepository
    {
        public ProfileMaterialRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }
    }
}
