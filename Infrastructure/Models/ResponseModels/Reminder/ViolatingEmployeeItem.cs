namespace Infrastructure.Models.ResponseModels.Reminder
{
    public class ViolatingEmployeeItem
    {
        public Guid Id { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeCode { get; set; }

        public string Reason { get; set; }

        public int NoOfRecidivism { get; set; }

        public DateTime Time { get; set; }

        public string HandlingMeasures { get; set; }
    }
}
