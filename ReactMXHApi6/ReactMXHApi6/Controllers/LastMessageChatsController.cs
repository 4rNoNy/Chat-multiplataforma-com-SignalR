using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using ReactMXHApi6.Core.Params;

namespace ReactMXHApi6.Controllers
{
    [Authorize]
    public class LastMessageChatsController : BaseApiController
    {
        // Obtém a lista de mensagens mais recentes de chats com base nos parâmetros de especificação.
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] LastMessageChatParams specParams)
        {
            // Define o nome de usuário atual com base na identidade do usuário autenticado.
            specParams.CurrentUserName = User.Identity.Name;
            // Obtém a lista de mensagens mais recentes de chats usando o repositório correspondente.
            var list = await UnitOfWork.LastMessageChatRepository.GetListLastMessageChat(specParams.CurrentUserName);
            // Retorna a lista como uma resposta HTTP 200 (OK).
            return Ok(list);
        }
    }
}
