using System.Linq;

namespace ProfOsmotr.DAL.Infrastructure
{
    internal class DBHelper
    {
        public static string NormalizeSQLiteNameSearchQuery(string query)
        {
            // SQLite не поддерживает регистронезависмый фильтр для кириллицы (без низкоуровневых танцев с бубном)
            // Эта нормализация не решает проблему, но повышает качество поиска

            if (string.IsNullOrEmpty(query))
                return string.Empty;

            string lower = query.ToLower();
            return lower.First().ToString().ToUpper() + lower.Substring(1);
        }
    }
}