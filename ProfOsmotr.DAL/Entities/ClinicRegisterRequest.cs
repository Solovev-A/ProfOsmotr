using System;

namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет запрос на регистрацию медицинской организации
    /// </summary>
    public class ClinicRegisterRequest
    {
        /// <summary>
        /// Идентификатор запроса
        /// </summary>
        public int Id { get; private set; }

        /// <summary>
        /// Полное наименование регистрируемой МО
        /// </summary>
        public string FullName { get; set; }

        /// <summary>
        /// Сокращенное наименование регистрируемой МО
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Адрес регистрируемой МО
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Телефон регистрируемой МО
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Электронная почта регистрируемой МО
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Идентификатор пользователя, отправившего запрос
        /// </summary>
        public int SenderId { get; set; }

        /// <summary>
        /// Пользователь, отправивший запрос
        /// </summary>
        public virtual User Sender { get; set; }

        /// <summary>
        /// Дата создания запроса
        /// </summary>
        public DateTime CreationTime { get; set; }       

        /// <summary>
        /// Отметка об обработке запроса
        /// </summary>
        public bool Processed { get; set; }

        /// <summary>
        /// Отметка об одобрении запроса
        /// </summary>
        public bool Approved { get; set; }
    }
}
