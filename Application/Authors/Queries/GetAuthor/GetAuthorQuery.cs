using Application.Abstractions.Messaging;
using Domain.Entities;

namespace Application.Authors.Queries.GetAuthor
{
    public sealed class GetAuthorQuery : IQuery<Author>
    {
        public Guid UserId { get; set; }

        public GetAuthorQuery(Guid userId) {
            UserId = userId;
        }
    }
}
