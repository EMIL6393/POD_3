using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using POD_3.DAL.Models;
using System.Net;

namespace POD_3.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected IActionResult GenerateSuccessResponse<T>(T data,string message = "")
        {
            var response = new DefaultResponseModel<T> { Data = data, Message = message,StatusCode = (int)HttpStatusCode.OK };
            return Ok(response);
        }

        protected IActionResult GenerateErrorResponse(List<ApiError> apiErrors,HttpStatusCode httpStatusCode = HttpStatusCode.BadRequest , string errorMessage="")
        {
            var response = new DefaultResponseModel<object> { Message = errorMessage,Errors =apiErrors, StatusCode =(int)httpStatusCode };
            return StatusCode((int)httpStatusCode, response);

        }


    }
}
