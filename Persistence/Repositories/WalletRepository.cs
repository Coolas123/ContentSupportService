using Domain.Entities;
using Domain.Repositories;

namespace Persistence.Repositories
{
    public class WalletRepository : BaseRepository<Wallet>, IWalletRepository
    {
        public WalletRepository(ApplicationDbContext dbContext) : base(dbContext) {
        }
    }
}
