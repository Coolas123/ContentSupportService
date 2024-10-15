using FluentValidation;
using System.Drawing;

namespace Application.Authors.Commands.ChangeSettings
{
    internal class ChangeAuthorSettingsCommandValidator : AbstractValidator<ChangeAuthorSettingsCommand>
    {
        public ChangeAuthorSettingsCommandValidator() {
            RuleFor(x => x.Description)
                .MaximumLength(256)
                .WithMessage("прозвище должно быть не длинее 256 символов");

            RuleFor(x => x.BannerFile)
                .Must(x => {
                    if (x == null) return true;

                    var image = Image.FromStream(x?.OpenReadStream());

                    if (image != null && (image.Width > 1900 || image?.Height > 250)) {
                        return false;
                    }
                    else return true;
                })
                .WithMessage("Баннер должен иметь разрешение не более чем 1900px на 250px");
        }
    }
}
