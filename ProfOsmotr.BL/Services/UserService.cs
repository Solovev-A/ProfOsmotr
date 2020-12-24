using AutoMapper;
using ProfOsmotr.Hashing;
using ProfOsmotr.BL.Abstractions;
using ProfOsmotr.DAL;
using ProfOsmotr.DAL.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ProfOsmotr.BL
{
    public class UserService : IUserService
    {
        #region Fields

        private readonly IMapper mapper;
        private readonly IPasswordHasher passwordHasher;
        private readonly IProfUnitOfWork uow;

        #endregion Fields

        #region Constructors

        public UserService(IMapper mapper, IProfUnitOfWork uow, IPasswordHasher passwordHasher)
        {
            this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
            this.passwordHasher = passwordHasher ?? throw new ArgumentNullException(nameof(passwordHasher));
        }

        #endregion Constructors

        #region Methods

        public async Task<UserResponse> AddUserAsync(int? clinicId, CreateUserRequest request)
        {
            if (request is null)
                return new UserResponse("Запрос не может быть пустым");
            if (!await CanSave(request.Username))
                return new UserResponse("Пользователь с таким username уже существует");
            Role role = await FindRole(request.RoleId);
            if (role == null)
                return new UserResponse("Роль не найдена");

            User newUser = new User()
            {
                UserProfile = new UserProfile()
                {
                    Name = request.Name,
                    Position = request.Position
                },
                Username = request.Username,
                PasswordHash = passwordHasher.HashPassword(request.Password),
                ClinicId = clinicId,
                Role = role
            };

            try
            {
                await uow.Users.AddAsync(newUser);
                await uow.SaveAsync();
                return new UserResponse(newUser);
            }
            catch (Exception ex)
            {
                return new UserResponse($"При добавлении пользователя произошла непредвиденная ошибка: {ex.Message}");
            }
        }

        public async Task<UserResponse> GetUser(int userId)
        {
            var user = await uow.Users.FindAsync(userId);
            if (user == null)
                return new UserResponse("Пользователь не найден");
            return new UserResponse(user);
        }

        public async Task<QueryResponse<User>> ListAllUsers<TKey>(int start,
                                                                  int length,
                                                                  string search,
                                                                  Expression<Func<User, TKey>> orderingSelector,
                                                                  bool descending)
        {
            try
            {
                QueryResult<User> users = await uow.Users.ExecuteQuery(orderingSelector, descending, start, length, search);
                return new QueryResponse<User>(users);
            }
            catch (Exception ex)
            {
                return new QueryResponse<User>(ex.Message);
            }
        }

        public async Task<QueryResponse<User>> ListClinicUsers<TKey>(int clinicId,
                                                                     int start,
                                                                     int length,
                                                                     string search,
                                                                     Expression<Func<User, TKey>> orderingSelector,
                                                                     bool descending)
        {
            try
            {
                QueryResult<User> users = await uow.Users.ExecuteQuery(orderingSelector,
                                                                       descending,
                                                                       start,
                                                                       length,
                                                                       search,
                                                                       u => u.ClinicId == clinicId);
                return new QueryResponse<User>(users);
            }
            catch (Exception ex)
            {
                return new QueryResponse<User>(ex.Message);
            }
        }

        public async Task<UserResponse> SetRoleAsync(int userId, int roleId)
        {
            var user = await uow.Users.FindAsync(userId);
            if (user == null)
                return new UserResponse("Пользователь не найден");

            Role role = await FindRole(roleId);
            if (role == null)
                return new UserResponse("Роль не найдена");
            
            user.Role = role;

            try
            {
                await uow.SaveAsync();
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"При изменении роли произошла непредвиденная ошибка: {ex.Message}");
            }
        }

        public async Task<UserResponse> UpdateUserAsync(int id, UpdateUserRequest request)
        {
            if (request is null)
                return new UserResponse("Запрос не может быть пустым");
            var user = await uow.Users.FindAsync(id);
            if (user == null)
                return new UserResponse("Пользователь не найден");
            if (!await CanSave(request.Username))
                return new UserResponse("Пользователь с таким username уже существует");
            Role role = null;
            if (request.RoleId.HasValue)
            {
                role = await FindRole(request.RoleId.Value);
                if (role == null)
                    return new UserResponse("Роль не найдена");
            }                

            //mapper.Map(request, user); 
            user.Role = role ?? user.Role;
            user.PasswordHash = request.Password == null ? user.PasswordHash : passwordHasher.HashPassword(request.Password);
            // Маппинг работает только на свойства объекта User. 
            // На вложенные объекты не удалось настроить игнорирование маппинга при источнике, равном null
            user.UserProfile.Name = request.Name ?? user.UserProfile.Name;
            user.UserProfile.Position = request.Position ?? user.UserProfile.Position;

            try
            {
                await uow.SaveAsync();
                return new UserResponse(user);
            }
            catch (Exception ex)
            {
                return new UserResponse($"При изменении пользователя произошла непредвиденная ошибка: {ex.Message}");
            }
        }

        public async Task<UserResponse> ValidatePassword(string username, string password)
        {
            var user = await uow.Users.FirstOrDefaultAsync(user => user.Username == username && user.RoleId != RoleId.Blocked);
            bool isPasswordValid = false;
            if (user != null)
            {
                isPasswordValid = passwordHasher.PasswordMatches(password, user.PasswordHash);
            }
            if (!isPasswordValid)
                return new UserResponse("Неверное имя пользователя или пароль");
            return new UserResponse(user);
        }

        private async Task<bool> CanSave(string username)
        {
            return await uow.Users.FirstOrDefaultAsync(u => u.Username == username) is null;
        }

        private async Task<Role> FindRole(int id)
        {
            if (Enum.IsDefined(typeof(RoleId), id))
                return await uow.Roles.FirstOrDefaultAsync(r => r.Id == (RoleId)id);
            return null;
        }

        #endregion Methods
    }
}