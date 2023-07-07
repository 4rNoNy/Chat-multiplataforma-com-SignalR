using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using ReactMXHApi6.Core.Entities;
using ReactMXHApi6.Core.Params;
using ReactMXHApi6.Dtos;
using ReactMXHApi6.SignalR;

namespace ReactMXHApi6.Controllers
{
    [Authorize]
    public class CommentController : BaseApiController
    {
        private readonly IHubContext<PresenceHub> _presenceHub;
        private readonly IMapper _mapper;

        public CommentController(IHubContext<PresenceHub> presenceHub, IMapper mapper)
        {
            _presenceHub = presenceHub;
            _mapper = mapper;
        }

        // Publica um novo comentário em um post específico.
        [HttpPost]
        public async Task<IActionResult> PostComment(string noidung, int postId)
        {
            var currentUser = await UnitOfWork.UserRepository.GetUserByUsernameAsync(User.Identity.Name);
            var postDb = await UnitOfWork.PostRepository.GetPostById(postId);

            if (postDb != null)
            {
                var comment = new Comment
                {
                    NoiDung = noidung,
                    User = currentUser,
                    Post = postDb,
                    PostId = postId
                };

                UnitOfWork.CommentRepository.Add(comment);
                if (await UnitOfWork.Complete())
                {
                    await _presenceHub.Clients.All.SendAsync("BroadcastComment", _mapper.Map<CommentDto>(comment));
                    return Ok();
                }
                else
                {
                    return BadRequest("Error while adding comment");
                }
            }
            else
            {
                return BadRequest("Post is null, cannot add comment");
            }
        }

        // Obtém os comentários paginados para um post específico.
        [HttpGet]
        public async Task<IActionResult> GetComments([FromQuery] CommentParams commentParams)
        {
            return Ok(await UnitOfWork.CommentRepository.GetCommentsPagination(commentParams));
        }
    }
}
