namespace ProfOsmotr.DAL
{
    /// <summary>
    /// Представляет целевую группу медицинского обследования
    /// </summary>
    public class TargetGroup
    {
        /// <summary>
        /// Идентификатор целевой группы
        /// </summary>
        public TargetGroupId Id { get; set; }
                
        /// <summary>
        /// Название целевой группы
        /// </summary>
        public string Name { get; set; }
    }
}