using ProfOsmotr.DAL.Abstractions;
using ProfOsmotr.Hashing;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProfOsmotr.DAL
{
    internal class UserSeeder
    {
        private readonly IProfUnitOfWork uow;
        private readonly IPasswordHasher passwordHasher;

        internal UserSeeder(IProfUnitOfWork uow, IPasswordHasher hasher)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
            this.passwordHasher = hasher ?? throw new ArgumentNullException(nameof(hasher));
        }

        /// <summary>
        /// Добавляет учетную запись администратора в хранилище
        /// </summary>
        /// <returns>Объект пользователя, представляющего собой администратора</returns>
        internal async Task<User> SeedAsync()
        {
            await SeedRolesAsync();

            var administrator = new User()
            {
                RoleId = RoleId.Administrator,
                Username = "Admin",
                PasswordHash = passwordHasher.HashPassword("12345"),
                UserProfile = new UserProfile()
                {
                    Name = "Администратор",
                    Position = string.Empty
                }
            };

            await uow.Users.AddAsync(administrator);

            return administrator;
        }

        private async Task SeedRolesAsync()
        {
            var roles = Enum.GetValues(typeof(RoleId))
                .Cast<RoleId>()
                .Select(e => new Role() { Id = e, Name = e.Description() });

            await uow.Roles.AddRangeAsync(roles);
        }
    }
}