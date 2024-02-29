using Infrastructure.Models.CommonModels;
using Infrastructure.Models.RequestModels.User;
using Infrastructure.Models.ResponseModels.User;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Utils.HttpResponseModels;

namespace HRMS.Controllers
{
    [Route("api/[controller]s")]
    [ApiController]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [Authorize(Policy = "Permission:Admin")]
        [HttpGet]
        public async Task<ActionResult<HttpResponse<List<UserResponse>>>> GetMany([FromQuery] UserFilterRequest req)
        {
            var (totalRecords, users) = await _userService.GetMany(req);

            return SuccessResponse(totalRecords, users);
        }

        [AllowAnonymous]
        [HttpGet("{employeeCode}")]
        public async Task<ActionResult<HttpResponse<UserResponse>>> GetEmployeeProfile(string employeeCode)
        {
            var user = await _userService.GetEmployeeProfile(employeeCode);

            return SuccessResponse(user);
        }

        [Authorize]
        [HttpGet("profile")]
        public async Task<ActionResult<HttpResponse<UserResponse>>> GetProfile()
        {
            var user = await _userService.GetProfile(Guid.Parse(User.FindFirst(ClaimTypes.Sid).Value));

            return SuccessResponse(user);
        }

        [AllowAnonymous]
        [HttpGet("pie-chart-statistics-employee-by-department")]
        public async Task<ActionResult<HttpResponse<List<PieChartStatisticsEmployeeByDepartmentItem>>>> GetPieChartStatisticsEmployeeByDepartment()
        {
            var result = await _userService.PieChartStatisticsEmployeeByDepartment();

            return SuccessResponse(result);
        }

        [AllowAnonymous]
        [HttpGet("pie-chart-statistics-employee-by-type")]
        public async Task<ActionResult<HttpResponse<List<PieChartStatisticsEmployeeByTypeItem>>>> GetPieChartStatisticsEmployeeByType()
        {
            var result = await _userService.PieChartStatisticsEmployeeByType();

            return SuccessResponse(result);
        }

        [AllowAnonymous]
        [HttpGet("line-chart-statistics-personnel-changes")]
        public async Task<ActionResult<HttpResponse<List<LineChartStatisticsPersonnelChanges>>>> GetLineChartStatisticsPersonnelChanges([FromQuery] string year)
        {
            var result = await _userService.LineChartStatisticsPersonnelChanges(year);

            return SuccessResponse(result);
        }

        [AllowAnonymous]
        [HttpGet("bar-chart-statistics-employee")]
        public async Task<ActionResult<HttpResponse<List<BarChartStatisticsTotalEmployeeItem>>>> GetBarChartStatisticsEmployee([FromQuery] string year)
        {
            var result = await _userService.BarChartStatisticsEmployee(year);

            return SuccessResponse(result);
        }

        [AllowAnonymous]
        [HttpGet("number-statistics-total-employee")]
        public async Task<ActionResult<HttpResponse<NumberStatistics>>> GetNumberStatisticsTotalEmployee([FromQuery] DateFilterWithPrev req)
        {
            var result = await _userService.NumberStatisticsTotalEmployee(req);

            return SuccessResponse(result);
        }
    }
}
