using Application.Abstractions.Messaging;
using Application.Authors.Commands.ChangeSettings;
using Application.Users.Commands.ChangeSettings;
using Domain.Shared;

namespace Application.Authors.Commands.ChangeAuthorAndUserSettings
{
    public sealed class ChangeAuthorAndUserSettingsCommand : ICommand<bool>
    {
        public ChangeAuthorSettingsCommand? ChangeAuthorSettingsCommand { get; set; }
        public ChangeUserSettingsCommand? ChangeUserSettingsCommand { get; set; }
    }
}
