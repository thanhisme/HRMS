using Infrastructure.Models.RequestModels.Contract;
using Infrastructure.Models.ResponseModels.Contract;

namespace Infrastructure.Services.Interfaces
{
    public interface IContractService
    {
        public Task<(int, List<ContractItem>)> GetListContracts(ContractFilterRequest req);

        public Task<ContractDetail> GetContractDetail(string code);

        public Task<Guid> CreateContract(Dictionary<string, dynamic> req);

        public Task<bool> CreateMultipleContracts(Dictionary<string, dynamic> req);

        public Task<Guid> UpdateContract(string code, Dictionary<string, dynamic> req);

        public Task<Guid> DeleteContract(string code);

        public Task<Guid> ExtendContract(string code);

        public Task<bool> ExtendMultipleContracts(List<string> codes);

        public Task<Guid> CancelContract(CancelContractRequest req);
    }
}
