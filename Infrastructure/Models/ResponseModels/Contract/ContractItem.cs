namespace Infrastructure.Models.RequestModels.Contract
{
    public class ContractItem
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string ContractType { get; set; }

        public string EmployeeName { get; set; }

        public string Position { get; set; }

        public int Duration { get; set; }

        public DateTime SignedAt { get; set; }
    }
}
