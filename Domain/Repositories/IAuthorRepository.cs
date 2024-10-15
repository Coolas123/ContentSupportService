using Domain.Entities;

namespace Domain.Repositories
{
    public interface IAuthorRepository : IBaseRepository<Author>
    {
        Task<Author> GetByUrlPage(string urlPage);
        Task<Author> GetByIdWithUserAndProfileMaterialsAsync(Guid id);
    }
}
