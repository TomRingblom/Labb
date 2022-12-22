using Microsoft.AspNetCore.Mvc;
// ReSharper disable Mvc.ViewNotResolved

namespace Labb.Features.Error
{
    [Route("error")]
    public class ErrorController : Controller
    {
        [Route("404")]
        public IActionResult Index()
        {
            return View();
        }
    }
}
