using Infrastructure.Models.RequestModels.Common;
using Infrastructure.Models.ResponseModels.Reminder;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utils.HttpResponseModels;

namespace HRMS.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class ReminderController : BaseController
    {
        private readonly IReminderService _reminderService;

        public ReminderController(IReminderService reminderService)
        {
            _reminderService = reminderService;
        }

        [AllowAnonymous]
        [HttpGet("contracts-about-to-expire")]
        public async Task<ActionResult<HttpResponse<List<ContractAboutToExpireItem>>>> GetListContractsAboutToExpire([FromQuery] PaginationFilterRequest req)
        {
            var (totalRecord, result) = await _reminderService.GetListContractsAboutToExpire(req);

            return SuccessResponse(totalRecord, result);
        }

        [AllowAnonymous]
        [HttpGet("violating-employees")]
        public async Task<ActionResult<HttpResponse<List<ViolatingEmployeeItem>>>> GetListViolatingEmployees([FromQuery] PaginationFilterRequest req)
        {
            var (totalRecord, result) = await _reminderService.GetListViolatingEmployees(req);

            return SuccessResponse(totalRecord, result);
        }

        [AllowAnonymous]
        [HttpGet("employee-with-birthdate-this-month")]
        public async Task<ActionResult<HttpResponse<List<EmployeeWithBirthdateThisMonthItem>>>> GetListEmployeeWithBirthdateThisMonth([FromQuery] PaginationFilterRequest req)
        {
            var (totalRecord, result) = await _reminderService.GetListEmployeeWithBirthdateThisMonth(req);

            return SuccessResponse(totalRecord, result);
        }

        [AllowAnonymous]
        [HttpGet("employee-have-not-signed-a-contract")]
        public async Task<ActionResult<HttpResponse<List<EmployeeHaveNotSignedAContractItem>>>> GetListEmployeeHaveNotSignedAContract([FromQuery] PaginationFilterRequest req)
        {
            var (totalRecord, result) = await _reminderService.GetListEmployeeHaveNotSignedAContract(req);

            return SuccessResponse(totalRecord, result);
        }
    }
}
