namespace Infrastructure.Models.RequestModels.Contract
{
    public class ExtendMultipleContractRequest
    {
        public List<string> codes { get; set; } = new();
    }
}
