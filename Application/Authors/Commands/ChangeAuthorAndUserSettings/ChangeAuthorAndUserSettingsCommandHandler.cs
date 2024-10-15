using Application.Abstractions.Messaging;
using Domain.Shared;
using MediatR;

namespace Application.Authors.Commands.ChangeAuthorAndUserSettings
{
    internal class ChangeAuthorAndUserSettingsCommandHandler : ICommandHandler<ChangeAuthorAndUserSettingsCommand, bool>
    {
        private readonly ISender sender;

        public ChangeAuthorAndUserSettingsCommandHandler(ISender sender) {
            this.sender = sender;
        }
        public async Task<Result<bool>> Handle(ChangeAuthorAndUserSettingsCommand request, CancellationToken cancellationToken) {
            var resultChangeUserSetting = await sender.Send(request.ChangeUserSettingsCommand!);

            if (resultChangeUserSetting.IsFailure) {
                return resultChangeUserSetting;
            }

            var resultChangeAuthorSetting = await sender.Send(request.ChangeAuthorSettingsCommand!);

            if (resultChangeAuthorSetting.IsFailure) {
                return resultChangeAuthorSetting;
            }

            return Result.Success(false);
        }
    }
}
