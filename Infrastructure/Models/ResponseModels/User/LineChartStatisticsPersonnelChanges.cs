namespace Infrastructure.Models.ResponseModels.User
{
    public class LineChartStatisticsPersonnelChangesItem
    {
        public string MonthCode { get; set; } = string.Empty;

        public string Month { get; set; } = string.Empty;

        public int Total { get; set; }
    }

    public class LineChartStatisticsPersonnelChanges
    {
        public string Title { get; set; }

        public List<LineChartStatisticsPersonnelChangesItem> Items { get; set; }
    }
}
