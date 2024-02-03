using Infrastructure.Models.RequestModels.User;
using Infrastructure.Models.ResponseModels.User;
using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
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
    }
}
