using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using ReactMXHApi6.Core.Entities;
using ReactMXHApi6.Core.Interfaces;
using ReactMXHApi6.Dtos;

namespace ReactMXHApi6.Infrastructure.Data
{
    public class MessageRepository : IMessageRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public MessageRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Adiciona um grupo ao contexto de dados
        public void AddGroup(Group group)
        {
            _context.Groups.Add(group);
        }

        // Adiciona uma mensagem ao contexto de dados
        public void AddMessage(Message message)
        {
            _context.Messages.Add(message);
        }

        // Obtém uma conexão com base no ID da conexão
        public async Task<Connection> GetConnection(string connectionId)
        {
            return await _context.Connections.FindAsync(connectionId);
        }

        // Obtém o grupo associado a uma determinada conexão
        public async Task<Group> GetGroupForConnection(string connectionId)
        {
            return await _context.Groups.Include(x => x.Connections)
                .Where(x => x.Connections.Any(c => c.ConnectionId == connectionId))
                .FirstOrDefaultAsync();
        }

        // Obtém o grupo de mensagens com base no nome do grupo
        public async Task<Group> GetMessageGroup(string groupName)
        {
            return await _context.Groups.Include(x => x.Connections)
                .FirstOrDefaultAsync(x => x.Name == groupName);
        }

        // Obtém a sequência de mensagens entre dois usuários em ordem ascendente
        public async Task<IEnumerable<MessageDto>> GetMessageThread(string currentUsername, string recipientUsername)
        {
            var messages = await _context.Messages
                .Where(m => (m.Recipient.UserName == currentUsername && m.Sender.UserName == recipientUsername) ||
                            (m.Recipient.UserName == recipientUsername && m.Sender.UserName == currentUsername))
                .OrderBy(m => m.MessageSent)
                .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var unreadMessages = messages.Where(m => m.DateRead == null && m.RecipientUsername == currentUsername).ToList();
            if (unreadMessages.Any())
            {
                foreach (var mess in unreadMessages)
                {
                    mess.DateRead = DateTime.Now;
                }
            }

            return messages;
        }

        // Obtém a sequência de mensagens entre dois usuários em ordem descendente
        public async Task<IEnumerable<MessageDto>> GetMessageThreadDescending(string currentUsername, string recipientUsername)
        {
            var messages = await _context.Messages
                .Where(m => (m.Recipient.UserName == currentUsername && m.Sender.UserName == recipientUsername) ||
                            (m.Recipient.UserName == recipientUsername && m.Sender.UserName == currentUsername))
                .OrderByDescending(m => m.MessageSent)
                .ProjectTo<MessageDto>(_mapper.ConfigurationProvider)
                .ToListAsync();

            var unreadMessages = messages.Where(m => m.DateRead == null && m.RecipientUsername == currentUsername).ToList();
            if (unreadMessages.Any())
            {
                foreach (var mess in unreadMessages)
                {
                    mess.DateRead = DateTime.Now;
                }
            }

            return messages;
        }

        // Remove uma conexão do contexto de dados
        public void RemoveConnection(Connection connection)
        {
            _context.Connections.Remove(connection);
        }
    }
}
