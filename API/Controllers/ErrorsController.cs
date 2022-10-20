using API.Infrastucture.Errors;
using EFModels.Data;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("errors/{code}")]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class ErrorsController : BaseApiController
    {
        public IActionResult Error(int code) {
            return new ObjectResult(new ApiResponse(code));
        }
    }
}