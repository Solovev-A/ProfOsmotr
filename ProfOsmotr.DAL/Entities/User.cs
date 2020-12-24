using System.Collections.Generic;

namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет пользователя
    /// </summary>
    public class User
    {
        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Идентификатор роли пользователя
        /// </summary>
        public RoleId RoleId { get; set; }

        /// <summary>
        /// Роль пользователя
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// Идентификато медицинской организации пользователя
        /// </summary>
        public int? ClinicId { get; set; }

        /// <summary>
        /// Медицинская организация пользователя
        /// </summary>
        public virtual Clinic Clinic { get; set; }

        /// <summary>
        /// Псевдоним пользователя
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Хеш пароля пользователя
        /// </summary>
        public string PasswordHash { get; set; }

        /// <summary>
        /// Профиль пользователя
        /// </summary>
        public virtual UserProfile UserProfile { get; set; }
        
        /// <summary>
        /// Расчеты, произведенные пользователем
        /// </summary>
        public virtual ICollection<Calculation> Calculations { get; } = new List<Calculation>();
    }
}