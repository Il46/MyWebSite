namespace MyWebSite.Models
{
    internal class CollegeSettings
    {
        public string CollegeName { get; private set; } = "Высший Колледж";
        public string AcademicYear { get; private set; } = "2026-2027";

        internal void UpdateSettings(string newName, string newYear)
        {
            if (string.IsNullOrWhiteSpace(newName) || string.IsNullOrWhiteSpace(newYear) ||
                newName.Length > 120 || newYear.Length > 20)
            {
                throw new System.ArgumentException("Название колледжа и учебный год обязательны.");
            }

            CollegeName = newName.Trim();
            AcademicYear = newYear.Trim();
        }
    }
}