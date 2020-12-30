using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace MutualFundsAPI.Helpers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CustomErrorHandler : ControllerBase
    {
        [Route("/error")]
        public IActionResult Error()
        {
            var errorResult = Content("Something broke on our end, we're working on it");
            HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;

            return errorResult;
        }
    }
}
