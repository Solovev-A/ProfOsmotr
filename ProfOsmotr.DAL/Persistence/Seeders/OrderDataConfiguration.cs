using System.IO;
using System.Reflection;

namespace ProfOsmotr.DAL
{
    public class OrderDataConfiguration
    {
        public static string OrderDataPath = GetPath("order.txt");

        public static string ExaminationsDataPath = GetPath("examinations.txt");

        public static string ICD10JsonPath = GetPath("ICD-10.json");

        private static string GetPath(string filePath)
        {
            string executionDirectoryPathName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\";
            return Path.Combine(executionDirectoryPathName, filePath);
        }
    }
}