using AutoMapper;
using Microsoft.EntityFrameworkCore;
using ReactMXHApi6.Core.Entities;
using ReactMXHApi6.Core.Interfaces;
using ReactMXHApi6.Dtos;

namespace ReactMXHApi6.Infrastructure.Data
{
    public class LastMessageChatRepository : ILastMessageChatRepository
    {
        private readonly DataContext _context;
        private readonly IMapper _mapper;

        public LastMessageChatRepository(DataContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        // Obtém o último registro de mensagem de chat entre dois usuários
        public async Task<LastMessageChat> GetLastMessageChat(string currentUsername, string recipientUsername)
        {
            var lastMessageChat = await _context.LastMessageChats.FirstOrDefaultAsync(x =>
                (x.SenderUsername == currentUsername && x.RecipientUsername == recipientUsername) ||
                (x.SenderUsername == recipientUsername && x.RecipientUsername == currentUsername));
            return lastMessageChat;
        }

        // Obtém uma lista de registros de última mensagem de chat para um usuário específico
        public async Task<List<LastMessageChatDto>> GetListLastMessageChat(string currentUsername)
        {
            var lastMessageChats = await _context.LastMessageChats
                .Include(x => x.Sender)
                .Include(x => x.Recipient)
                .Where(x => x.GroupName!.Contains(currentUsername))
                .ToListAsync();
            return _mapper.Map<List<LastMessageChat>, List<LastMessageChatDto>>(lastMessageChats);
        }

        // Obtém o número de mensagens não lidas para um usuário específico
        public async Task<int> GetUnread(string currentUsername)
        {
            var unreadMessages = await _context.LastMessageChats
                .Where(x => x.GroupName!.Contains(currentUsername) && x.IsRead == false)
                .ToListAsync();
            return unreadMessages.Count;
        }

        // Atualiza um registro de última mensagem de chat
        public void Update(LastMessageChat lastMessageChat)
        {
            _context.Entry(lastMessageChat).State = EntityState.Modified;
        }

        // Adiciona um novo registro de última mensagem de chat
        public void Add(LastMessageChat lastMessageChat)
        {
            _context.LastMessageChats.Add(lastMessageChat);
        }
    }
}
