using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReactMXHApi6.Core.Entities;
using ReactMXHApi6.Core.Interfaces;
using ReactMXHApi6.Core.Params;
using ReactMXHApi6.Dtos;
using ReactMXHApi6.Helper;

namespace ReactMXHApi6.Infrastructure.Data
{
    public class CommentRepository : ICommentRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public CommentRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Adiciona um comentário ao contexto do banco de dados
        public void Add(Comment comment)
        {
            _context.Comments.Add(comment);
        }

        // Obtém uma página de comentários com base nos parâmetros fornecidos
        public async Task<Pagination<CommentDto>> GetCommentsPagination(CommentParams commentParams)
        {
            // Cria uma consulta no contexto de dados para obter os comentários com o ID de postagem fornecido
            var query = _context.Comments.Where(cmt => cmt.PostId == commentParams.PostId)
                .Include(x => x.User)
                .AsQueryable();

            // Conta o número total de itens na consulta
            var totalItems = await query.CountAsync();

            // Obtém uma lista de comentários paginada com base nos parâmetros fornecidos
            var list = await query.Skip((commentParams.PageNumber - 1) * commentParams.PageSize)
                .Take(commentParams.PageSize)
                .ToListAsync();

            // Mapeia a lista de comentários para uma lista de DTOs de comentários
            var items = _mapper.Map<List<Comment>, List<CommentDto>>(list);

            // Retorna uma instância de Pagination<CommentDto> contendo as informações da página atual e os itens correspondentes
            return new Pagination<CommentDto>(commentParams.PageNumber, commentParams.PageSize, totalItems, items);
        }
    }
}
