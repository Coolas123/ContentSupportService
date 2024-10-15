using Domain.Entities;
using Domain.Shared;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

namespace Domain.EntityServices.UserServices
{
    public interface ISaveProfileMaterial
    {
        Task<Result> Save(IFormFile avatar, Guid userId, string path, IWebHostEnvironment webHostEnvironment);
    }
}
