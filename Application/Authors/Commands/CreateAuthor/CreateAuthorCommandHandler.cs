using Application.Abstractions.Messaging;
using Domain.Entities;
using Domain.EntityServices.AuthorServices;
using Domain.Errors;
using Domain.Repositories;
using Domain.Shared;

namespace Application.Authors.Commands.RegisterAuthor
{
    public sealed class CreateAuthorCommandHandler : ICommandHandler<CreateAuthorCommand, Author>
    {
        private readonly IUrlPageUniqueCheck urlPageUniqueCheck;
        private readonly IAuthorRepository authorRepository;
        private readonly IUnitOfWork unitOfWork;

        public CreateAuthorCommandHandler(IUrlPageUniqueCheck urlPageUniqueCheck, IAuthorRepository authorRepository,
            IUnitOfWork unitOfWork) {
            this.urlPageUniqueCheck = urlPageUniqueCheck;
            this.authorRepository = authorRepository;
            this.unitOfWork = unitOfWork;
        }

        public async Task<Result<Author>> Handle(CreateAuthorCommand request, CancellationToken cancellationToken) {
            bool pageExist = await urlPageUniqueCheck.IsUnique(request.UrlPage);

            if (pageExist) {
                return Result.Failure<Author>(DomainError.Author.UrlPageIsArleadyUsed);
            }

            Author author = new Author(request.UserId, request.UrlPage); 

            await authorRepository.CreateAsync(author);

            await unitOfWork.SaveChangesAsync();

            return Result.Success(author);
        }
    }
}
