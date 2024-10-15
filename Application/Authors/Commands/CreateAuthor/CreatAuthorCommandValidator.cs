using FluentValidation;

namespace Application.Authors.Commands.RegisterAuthor
{
    internal class CreatAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreatAuthorCommandValidator() {
            RuleFor(x => x.UrlPage)
                .NotEmpty()
                .WithMessage("Введите адрес вашей страницы");
        }
    }
}
