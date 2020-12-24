namespace ProfOsmotr.Hashing
{
    /// <summary>
    /// Представляет абстракцию для хеширования паролей
    /// </summary>
    public interface IPasswordHasher
    {
        /// <summary>
        /// Возвращает хешированное значение предоставляемого пароля
        /// </summary>
        /// <param name="password" >Пароль для хеширования</param>
        /// <returns>Хешированное значение предоставляемого пароля</returns>
        string HashPassword(string password);

        /// <summary>
        /// Возвращает результат сравнения хешей паролей
        /// </summary>
        /// <param name="providedPassword">Пароль для сравнения</param>
        /// <param name="passwordHash">Сохраненное хешированное значение пароля пользователя</param>
        /// <returns>true, если <paramref name="passwordHash"/> верен, иначе false</returns>
        bool PasswordMatches(string providedPassword, string passwordHash);
    }
}