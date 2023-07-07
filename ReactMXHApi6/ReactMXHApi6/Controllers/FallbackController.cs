using Microsoft.AspNetCore.Mvc;

namespace ReactMXHApi6.Controllers
{
    public class FallbackController : Controller
    {
        // Retorna o arquivo index.html localizado na pasta wwwroot como resposta.
        public IActionResult Index()
        {
            return PhysicalFile(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "index.html"), "text/HTML");
        }
    }
}
