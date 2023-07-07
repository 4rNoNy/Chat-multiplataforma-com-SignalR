using AutoMapper;
using ReactMXHApi6.Core.Interfaces;
using ReactMXHApi6.Infrastructure.Services;

namespace ReactMXHApi6.Infrastructure.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public UnitOfWork(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Retorna uma instância do repositório de comentários
        public ICommentRepository CommentRepository => new CommentRepository(_context, _mapper);

        // Retorna uma instância do repositório do OneSignal
        public IOneSignalRepository OneSignalRepository => new OneSignalRepository(_context);

        // Retorna uma instância do repositório de posts
        public IPostRepository PostRepository => new PostRepository(_context, _mapper);

        // Retorna uma instância do repositório de última mensagem do chat
        public ILastMessageChatRepository LastMessageChatRepository => new LastMessageChatRepository(_context, _mapper);

        // Retorna uma instância do repositório de usuários
        public IUserRepository UserRepository => new UserRepository(_context, _mapper);

        // Retorna uma instância do repositório de mensagens
        public IMessageRepository MessageRepository => new MessageRepository(_context, _mapper);

        // Libera os recursos do contexto de dados
        public void Dispose()
        {
            _context.Dispose();
        }

        // Completa a transação e salva as alterações no banco de dados
        public async Task<bool> Complete()
        {
            return await _context.SaveChangesAsync() > 0;
        }

        // Verifica se há alterações pendentes no contexto de dados
        public bool HasChanges()
        {
            return _context.ChangeTracker.HasChanges();
        }
    }
}
