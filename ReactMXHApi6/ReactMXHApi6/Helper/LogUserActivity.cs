using Microsoft.AspNetCore.Mvc.Filters;
using ReactMXHApi6.Core.Interfaces;
using ReactMXHApi6.Extensions;

namespace ReactMXHApi6.Helper
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            // Executa o filtro antes e depois da execução da ação

            // Executa a próxima ação no pipeline
            var resultContext = await next();

            // Verifica se o usuário está autenticado
            if (!resultContext.HttpContext.User.Identity.IsAuthenticated) return;

            // Obtém o ID do usuário autenticado
            var userId = resultContext.HttpContext.User.GetUserId();

            // Obtém a instância do repositório a partir do serviço
            var repo = resultContext.HttpContext.RequestServices.GetService<IUnitOfWork>();

            // Obtém o usuário do repositório pelo ID
            var user = await repo.UserRepository.GetUserByIdAsync(userId);

            // Atualiza o campo LastActive do usuário
            user.LastActive = DateTime.Now;

            // Completa a transação do repositório
            await repo.Complete();
        }
    }
}
