using API.Infrastucture.Errors;
using EFModels;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/buggy")]
    public class ErrorController : BaseApiController
    {
        private readonly StoreContext _dbContext;

        public ErrorController(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }

         [HttpGet("badrequest")]
        public ActionResult GetBadRequest()
        {

            return BadRequest(new ApiResponse(400));
        }

        [HttpGet("badrequest/{id}")]
        public ActionResult GetBadRequest(int id)
        {

            return Ok();
        }


        [HttpGet("notfound")]
        public ActionResult GetNotFound(){

            return NotFound(new ApiResponse(404));
            
        }

        [HttpGet("servererror")]
        public ActionResult GetServerError(){

            var record = _dbContext.Products.Find(333);

            var  ret = record.ToString();

            return Ok();
        }
    }
}