using Domain.EntityServices.UserServices;
using Domain.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Application.DomainServices.UserServices
{
    public sealed class SaveProfileMaterial : ISaveProfileMaterial
    {
        public async Task<Result> Save(IFormFile avatar, Guid userId, string path,IWebHostEnvironment webHostEnvironment) {

            try {
                using (var fStream = new FileStream(webHostEnvironment.WebRootPath + path, FileMode.Create)) {
                    await avatar.CopyToAsync(fStream);
                }
                return Result.Success();
            }
            catch {
                return Result.Failure();
            }
        }
    }
}
