using Domain.EntityServices.AuthorServices;
using Domain.Repositories;

namespace Application.DomainServices.AuthorServices
{
    public sealed class UrlPageUniqueCheck : IUrlPageUniqueCheck
    {
        private readonly IAuthorRepository authorRepository;

        public UrlPageUniqueCheck(IAuthorRepository authorRepository)
        {
            this.authorRepository = authorRepository;
        }

        public async Task<bool> IsUnique(string urlPage)
        {
            var dbAuthor = await authorRepository.GetByUrlPage(urlPage);
            return dbAuthor != null;
        }
    }
}
