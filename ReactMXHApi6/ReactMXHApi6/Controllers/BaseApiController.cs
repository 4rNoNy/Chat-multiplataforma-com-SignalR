using Microsoft.AspNetCore.Mvc;
using ReactMXHApi6.Core.Interfaces;
using ReactMXHApi6.Helper;

namespace ReactMXHApi6.Controllers
{
    // Aplica o filtro de serviço "LogUserActivity" a todos os métodos do controlador.
    [ServiceFilter(typeof(LogUserActivity))]
    [Route("api/[controller]")]
    [ApiController]
    public class BaseApiController : ControllerBase
    {
        private IUnitOfWork _unitOfWork;

        // Propriedade protegida que fornece acesso à instância de IUnitOfWork.
        // Se _unitOfWork for nulo, obtém a instância do serviço IUnitOfWork por meio do HttpContext.
        protected IUnitOfWork UnitOfWork => _unitOfWork ??= HttpContext.RequestServices.GetService<IUnitOfWork>();
    }
}
