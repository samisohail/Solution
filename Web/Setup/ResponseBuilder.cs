using DataTransferObjects;
using Microsoft.AspNetCore.Mvc;

namespace Web.Setup
{
    public static class BuildResponseExtensions
    {
        public static IActionResult BuildResponse(this Result result)
        {
            var error = IsError(result);

            return error ?? new OkResult();
        }

        public static IActionResult BuildResponse<T>(this Result<T> result)
        {
            var error = IsError(result);

            return error ?? new OkObjectResult(result.Value);
        }

        private static IActionResult IsError(Result result)
        {
            // some error occurred
            if (!result.Success && result.NotFound != true)
                return new BadRequestObjectResult(result.Error);

            // an expected item not found
            if (!result.Success && result.NotFound == true)
                return new NotFoundObjectResult(result.Error);

            // no error
            return null;
        }

    }
}
