using Infrastructure.Models.RequestModels.Contract;
using Infrastructure.Models.ResponseModels.Contract;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utils.HttpResponseModels;

namespace HRMS.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ContractController : BaseController
    {
        private readonly IContractService _contractService;

        public ContractController(IContractService contractService)
        {
            _contractService = contractService;
        }

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<HttpResponse<List<ContractItem>>>> GetListContractsAboutToExpire([FromQuery] ContractFilterRequest req)
        {
            var (totalRecord, result) = await _contractService.GetListContracts(req);

            return SuccessResponse(totalRecord, result);
        }

        [AllowAnonymous]
        [HttpGet("{code}")]
        public async Task<ActionResult<HttpResponse<ContractDetail>>> GetContractDetail(string code)
        {
            var result = await _contractService.GetContractDetail(code);

            return SuccessResponse(result);
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<ActionResult<HttpResponse<Guid>>> CreateContract([FromBody] Dictionary<string, dynamic> req)
        {
            var result = await _contractService.CreateContract(req);

            return SuccessResponse(result);
        }

        [AllowAnonymous]
        [HttpPost("multiple-contracts")]
        public async Task<ActionResult<HttpResponse<bool>>> CreateMultipleContracts([FromBody] Dictionary<string, dynamic> req)
        {
            var result = await _contractService.CreateMultipleContracts(req);

            return SuccessResponse(result);
        }

        [AllowAnonymous]
        [HttpPatch("{code}")]
        public async Task<ActionResult<HttpResponse<Guid>>> UpdateContract(string code, [FromBody] Dictionary<string, dynamic> req)
        {
            var result = await _contractService.UpdateContract(code, req);

            return SuccessResponse(result);
        }

        [AllowAnonymous]
        [HttpDelete("{code}")]
        public async Task<ActionResult<HttpResponse<Guid>>> DeleteContract(string code)
        {
            var result = await _contractService.DeleteContract(code);

            return SuccessResponse(result);
        }

        [AllowAnonymous]
        [HttpPatch("{code}/extend")]
        public async Task<ActionResult<HttpResponse<Guid>>> ExtendContract(string code)
        {
            var result = await _contractService.ExtendContract(code);

            return SuccessResponse(result);
        }

        [AllowAnonymous]
        [HttpPatch("multiple-contracts/extend")]
        public async Task<ActionResult<HttpResponse<bool>>> ExtendMultipleContracts([FromBody] ExtendMultipleContractRequest req)
        {
            var result = await _contractService.ExtendMultipleContracts(req.codes);

            return SuccessResponse(result);
        }

        [AllowAnonymous]
        [HttpPatch("{code}/cancel")]
        public async Task<ActionResult<HttpResponse<Guid>>> CancelContract(CancelContractRequest req)
        {
            var result = await _contractService.CancelContract(req);

            return SuccessResponse(result);
        }
    }
}
