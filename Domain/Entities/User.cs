using Domain.EntityServices;
using Domain.Enums;
using Domain.Errors;
using Domain.Primitives;
using Domain.Shared;
using System.Security.Claims;

namespace Domain.Entities
{
    public sealed class User : Entity
    {
        private readonly List<ProfileMaterial> _profileMaterials = new();

        public string UserName { get; private set; }
        public DateTime DateOfBirth { get; private set; }
        public string Email { get; private set; }
        public string Gender { get; private set; }
        public string HashPassword { get; private set; }
        public SystemRole SystemRole { get; private set; }
        public UserRole UserRole { get; private set; }
        public Country Country { get; private set; }
        public byte WarnAmount { get; private set; }
        public bool IsBan { get; }
        public Wallet Wallet { get; }
        public IReadOnlyCollection<ProfileMaterial>? ProfileMaterials => _profileMaterials;

        private User(Guid id,
            string userName,
            DateTime dateOfBirth,
            string email,
            string gender,
            string hashPassword,
            SystemRole systemRole,
            UserRole userRole,
            Country country)
            : base(id) {
            UserName = userName;
            DateOfBirth = dateOfBirth;
            Email = email;
            Gender = gender;
            HashPassword = hashPassword;
            SystemRole = systemRole;
            UserRole = userRole;
            Country = country;
        }

        public async static Task<Result<User>> CreateAsync(
            Guid id,
            string userName,
            DateTime dateOfBirth,
            string email,
            string gender,
            string hashPassword,
            SystemRole systemRole,
            UserRole userRole,
            Country country,
            IEmailUniqueCheck emailUniqueCheck) 
            {

            if(!await emailUniqueCheck.IsUnique(email)) {
                return Result.Failure<User>(DomainError.UserError.EmailIsArleadyUsed);
            }

            return Result.Success(
                    new User
                    (id,
                    userName,
                    dateOfBirth,
                    email,
                    gender,
                    hashPassword,
                    systemRole,
                    userRole,
                    country));
        }

        public Result<ClaimsIdentity> Authenticate() {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType,Email),
                new Claim("Id",Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,
                ((SystemRole)Enum
                .GetValues(typeof(SystemRole))
                .GetValue((int)SystemRole-1))
                .ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType,
                ((UserRole)Enum
                .GetValues(typeof(UserRole))
                .GetValue((int)UserRole-1))
                .ToString())
            };

            var claimIdentity = new ClaimsIdentity(claims, "ApplicationCookie",
                ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);

            return new Result<ClaimsIdentity>(claimIdentity, true, Error.None);
        }

        public void ChangeUserRole(UserRole userRole) {
            UserRole = userRole;
        }

        public bool ChangeSettings(
            string? userName,
            DateTime? dateOfBirth,
            string? email,
            Country? country,
            string hashPassword
            ) {
            bool isClaimIdentitiesChanged = false;

            if (userName != null && userName != UserName) {
                UserName=userName;
            }
            if (dateOfBirth != null && dateOfBirth != DateOfBirth) {
                DateOfBirth = (DateTime)dateOfBirth;
            }
            if (email != null && email != Email) {
                Email = email;
                isClaimIdentitiesChanged = true;
            }
            if (country != null && country != Country) {
                Country = (Country)country;
            }
            if (hashPassword != null && hashPassword != HashPassword) {
                HashPassword = hashPassword;
                isClaimIdentitiesChanged = true;
            }

            return isClaimIdentitiesChanged;
        }

        public void ChangeProfileMaterial(ProfileMaterial profileMaterial, MaterialType materialType) {
            var replaceable = ProfileMaterials.FirstOrDefault(x => x.MaterialType == materialType);

            if(replaceable!= null) {
                _profileMaterials.Remove(replaceable);
            }

            _profileMaterials.Add(profileMaterial);
        }
    }
}
