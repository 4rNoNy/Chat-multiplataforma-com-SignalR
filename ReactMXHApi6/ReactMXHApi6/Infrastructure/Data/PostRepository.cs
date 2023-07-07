using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReactMXHApi6.Core.Entities;
using ReactMXHApi6.Core.Interfaces;
using ReactMXHApi6.Core.Params;
using ReactMXHApi6.Dtos;
using ReactMXHApi6.Helper;

namespace ReactMXHApi6.Infrastructure.Data
{
    public class PostRepository : IPostRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public PostRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Obtém um post pelo seu ID
        public async Task<Post> GetPostById(int postId)
        {
            return await _context.Posts.SingleOrDefaultAsync(x => x.Id == postId);
        }

        // Adiciona um post ao contexto de dados
        public void Add(Post message)
        {
            _context.Posts.Add(message);
        }

        // Obtém a paginação de posts com base nos parâmetros de paginação fornecidos
        public async Task<Pagination<PostDto>> GetPostsPagination(PostParams postParams)
        {
            var query = _context.Posts
                .Include(x => x.Images)
                .Include(x => x.User)
                .Include(x => x.Comments)
                .ThenInclude(x => x.User)
                .AsQueryable();

            var totalItems = await query.CountAsync();

            var list = await query.Skip((postParams.PageNumber - 1) * postParams.PageSize)
                .Take(postParams.PageSize)
                .ToListAsync();

            var items = _mapper.Map<List<Post>, List<PostDto>>(list);

            return new Pagination<PostDto>(postParams.PageNumber, postParams.PageSize, totalItems, items);
        }
    }
}
