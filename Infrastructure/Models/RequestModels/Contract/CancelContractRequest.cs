namespace Infrastructure.Models.RequestModels.Contract
{
    public class CancelContractRequest
    {
        public string ContractCode { get; set; }

        public string Reason { get; set; }
    }
}
