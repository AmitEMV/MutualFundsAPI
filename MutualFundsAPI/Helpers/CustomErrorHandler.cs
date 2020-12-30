using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MutualFundsAPI.Helpers
{
    [ApiController]
    [ApiExplorerSettings(IgnoreApi = true)]
    public class CustomErrorHandler : ControllerBase
    {
        private readonly ILogger<CustomErrorHandler> _logger;
        public CustomErrorHandler(ILogger<CustomErrorHandler> logger)
        {
            _logger = logger;
        }

        [Route("/error")]
        public IActionResult Error()
        {
            var exception = HttpContext.Features.Get<IExceptionHandlerPathFeature>();
            _logger.LogError(exception.Error.Message);
            _logger.LogError(exception.Error.StackTrace);

            var errorResult = Content("Something broke on our end, we're working on it");
            HttpContext.Response.StatusCode = (int)System.Net.HttpStatusCode.InternalServerError;

            return errorResult;
        }
    }
}
