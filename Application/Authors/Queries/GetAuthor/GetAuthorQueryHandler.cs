using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Authors.Queries.GetAuthor
{
    public sealed class GetAuthorQueryHandler : IQueryHandler<GetAuthorQuery, Author>
    {
        private readonly IAuthorRepository authorRepository;

        public GetAuthorQueryHandler(IAuthorRepository authorRepository) {
            this.authorRepository = authorRepository;
        }
        public async Task<Result<Author>> Handle(GetAuthorQuery request, CancellationToken cancellationToken) {
            var author = await authorRepository.GetByIdWithUserAndProfileMaterialsAsync(request.UserId);

            if (author != null) {
                return Result.Success(author);
            }

            return Result.Failure<Author>(PersistenceError.Author.AuthorNotFound);
        }
    }
}
