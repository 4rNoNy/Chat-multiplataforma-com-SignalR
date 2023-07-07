using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ReactMXHApi6.Controllers
{
    [Authorize]
    public class UserController : BaseApiController
    {
        // Obtém os detalhes do usuário
        [HttpGet("{username}")]
        public async Task<IActionResult> GetMember(string username)
        {
            return Ok(await UnitOfWork.UserRepository.GetMemberAsync(username));
        }
    }
}
