using Domain.Enums;
using Domain.Primitives;
using Domain.Shared;

namespace Domain.Entities
{
    public sealed class ProfileMaterial : Entity
    {
        public Guid UserId { get; set; }
        public MaterialType MaterialType { get; set; }
        public string Title { get; set; }
        public string Path { get; set; }

        public ProfileMaterial(Guid id, Guid userId, MaterialType materialType, string title, string path) 
            : base(id) {
            UserId= userId;
            MaterialType = materialType;
            Title = title;
            Path = path;
        }

        public static Result<ProfileMaterial> Create(Guid id,Guid userId, MaterialType materialType, string title, string path) {
            return Result.Success(new ProfileMaterial(id, userId, materialType, title, path));
        }
    }
}
