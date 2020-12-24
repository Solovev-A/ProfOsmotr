using System.Collections.Generic;

namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет медицинскую организацию
    /// </summary>
    public class Clinic
    {
        /// <summary>
        /// Идентификатор МО
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Значение статуса блокировки МО
        /// </summary>
        public bool IsBlocked { get; set; }

        /// <summary>
        /// Подробное описание МО
        /// </summary>
        public virtual ClinicDetails ClinicDetails { get; set; }

        /// <summary>
        /// Коллекция актуальных услуг МО
        /// </summary>
        public virtual ICollection<ActualClinicService> ActualClinicServices { get; } = new List<ActualClinicService>();

        /// <summary>
        /// Коллекция всех услуг МО
        /// </summary>
        public virtual ICollection<Service> Services { get; } = new List<Service>();

        /// <summary>
        /// Коллекция сохраненных расчетов медицинского осмотра, произведенных в МО
        /// </summary>
        public virtual ICollection<Calculation> Calculations { get; } = new List<Calculation>();

        /// <summary>
        /// Коллекция пользователей МО
        /// </summary>
        public virtual ICollection<User> Users { get; } = new List<User>();
    }
}