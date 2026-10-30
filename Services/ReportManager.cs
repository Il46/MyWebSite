namespace MyWebSite.Services
{
    public class ReportManager
    {
        protected internal string GenerateReport(string reportData)
        {
            return $"--- ОТЧЕТ КОЛЛЕДЖА ---\n{reportData}";
        }
    }
}