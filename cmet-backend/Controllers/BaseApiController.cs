using cmet_backend.Common;
using Microsoft.AspNetCore.Mvc;

namespace cmet_backend.Controllers
{
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        protected IActionResult ApiOk(object? data)
        {
            return Ok(ApiResponse.success(data));
        }

        protected IActionResult ApiOk()
        {
            return Ok(ApiResponse.success());
        }

        protected IActionResult ApiError(string code, string message, int statusCode = 400)
        {
            return StatusCode(statusCode, ApiResponse.fail(code, message));
        }
    }
}
