using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReactMXHApi6.Dtos;

namespace ReactMXHApi6.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BuggyController : ControllerBase
    {
        // Retorna uma resposta HTTP 404 (Not Found).
        [HttpGet("not-found")]
        public ActionResult GetNotFound()
        {
            return NotFound();
        }

        // Retorna uma resposta HTTP 400 (Bad Request) com uma mensagem de erro.
        [HttpGet("bad-request")]
        public ActionResult GetBadRequest()
        {
            return BadRequest("You have a bad request");
        }

        // Gera uma exceção intencional para simular um erro do servidor.
        [HttpGet("server-error")]
        public ActionResult GetServerError()
        {
            throw new Exception("This is a server error");
        }

        // Retorna uma resposta HTTP 401 (Unauthorized).
        [HttpGet("unauthorised")]
        public ActionResult GetUnauthorised()
        {
            return Unauthorized();
        }

        // Retorna uma resposta HTTP 200 (OK) com uma carga útil vazia.
        [HttpPost("validation-error")]
        public ActionResult GetValidationError(LoginDto loginDto)
        {
            return Ok();
        }
    }
}
