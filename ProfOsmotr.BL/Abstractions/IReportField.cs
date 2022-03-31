namespace ProfOsmotr.BL.Abstractions
{
    /// <summary>
    /// Представляет абстракцию для поля отчета
    /// </summary>
    public interface IReportField
    {
        /// <summary>
        /// Представляет значение поля отчета
        /// </summary>
        string Value { get; }
    }
}