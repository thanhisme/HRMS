namespace Infrastructure.Models.ResponseModels.User
{
    public class PieChartStatisticsEmployeeByDepartmentItem
    {
        public Guid DepartmentId { get; set; } = Guid.NewGuid();

        public string DepartmentName { get; set; } = string.Empty;

        public int TotalCount { get; set; }
    }
}
