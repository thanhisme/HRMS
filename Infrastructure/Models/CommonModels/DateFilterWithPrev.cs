namespace Infrastructure.Models.CommonModels
{
    public class DateFilterWithPrev
    {
        public string startDate { get; set; }

        public string endDate { get; set; }

        public string? prevUnitStartDate { get; set; } = null;

        public string? prevUnitEndDate { get; set; } = null;
    }
}
