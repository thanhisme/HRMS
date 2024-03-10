namespace Infrastructure.Models.ResponseModels.Reminder
{
    public class ContractAboutToExpireItem
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string EmployeeName { get; set; }

        public string EmployeeCode { get; set; }

        public string ContractType { get; set; }

        public int Duration { get; set; }

        public DateTime Expiry { get; set; }
    }
}
