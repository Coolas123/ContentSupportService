using Application.Abstractions.Messaging;
using Domain.Enums;
using Domain.Shared;
using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace Application.Authors.Commands.ChangeSettings
{
    public sealed class ChangeAuthorSettingsCommand : ICommand<bool>
    {
        [Display(Name = "Баннер")]
        public IFormFile? BannerFile { get; set; }

        [Display(Name = "Описание")]
        public string? Description { get; set; }

        public Guid UserId {  get; set; }
    }
}
