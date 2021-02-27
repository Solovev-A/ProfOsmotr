using System.IO;
using System.Reflection;

namespace ProfOsmotr.DAL
{
    public class OrderDataConfiguration
    {
        public static string OrderDataJsonPath = GetPath("orderData.json");

        public static string ExaminationsDataJsonPath = GetPath("examinationsData.json");

        public static string ICD10JsonPath = GetPath("ICD-10.json");

        private static string GetPath(string filePath)
        {
            string executionDirectoryPathName = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\";
            return Path.Combine(executionDirectoryPathName, filePath);
        }
    }
}