using Foundations.Core;

namespace Domain.Enumerations
{
    public class Duration : Enumeration
    {
        public static Duration Annual { get; } = new Duration(1, "Annual");
        public static Duration Monthly { get; } = new Duration(12, "Monthly");
        public static Duration FortNightly { get; } = new Duration(26, "FortNightly");
        public static Duration Weekly { get; } = new Duration(52, "Weekly");

        public int TimesPerYear => Id;

        private Duration(int id, string name) : base(id, name)
        {
            
        }
    }
}