using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;
using ReactMXHApi6.Core.Entities;
using ReactMXHApi6.Core.Interfaces;
using ReactMXHApi6.Dtos;

namespace ReactMXHApi6.SignalR
{
    [Authorize]
    public class MessageHub : Hub
    {
        private readonly IMapper _mapper;
        private readonly IHubContext<PresenceHub> _presenceHub;
        private readonly PresenceTracker _presenceTracker;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IOneSignalService _oneSignalService;
        private readonly IConfiguration _config;

        public MessageHub(IMapper mapper,
            IUnitOfWork unitOfWork,
            IHubContext<PresenceHub> presenceHub,
            PresenceTracker presenceTracker,
            IOneSignalService oneSignalService, IConfiguration config)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _presenceTracker = presenceTracker;
            _presenceHub = presenceHub;
            _oneSignalService = oneSignalService;
            _config = config;
        }

        // Chamado quando um cliente se conecta ao hub
        public override async Task OnConnectedAsync()
        {
            var httpContext = Context.GetHttpContext();
            var otherUser = httpContext.Request.Query["user"].ToString();
            var groupName = GetGroupName(Context.User.Identity.Name, otherUser);
            await Groups.AddToGroupAsync(Context.ConnectionId, groupName);
            var group = await AddToGroup(groupName);

            var messages = await _unitOfWork.MessageRepository.GetMessageThread(Context.User.Identity.Name, otherUser);

            await Clients.Caller.SendAsync("ReceiveMessageThread", messages);
        }

        // Chamado quando um cliente é desconectado do hub
        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var group = await RemoveFromMessageGroup();
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, group.Name);
            await base.OnDisconnectedAsync(exception);
        }

        // Envia uma mensagem para outro usuário
        public async Task SendMessage(CreateMessageDto createMessageDto)
        {
            var userName = Context.User.Identity.Name;
            if (userName == createMessageDto.RecipientUsername.ToLower())
                throw new HubException("Você não pode enviar mensagem para si mesmo");

            var sender = await _unitOfWork.UserRepository.GetUserByUsernameAsync(userName);
            var recipient = await _unitOfWork.UserRepository.GetUserByUsernameAsync(createMessageDto.RecipientUsername);
            if (recipient == null) throw new HubException("Usuário não encontrado");

            var message = new Message
            {
                Sender = sender,
                Recipient = recipient,
                SenderUsername = sender.UserName,
                RecipientUsername = recipient.UserName,
                Content = createMessageDto.Content
            };
            var groupName = GetGroupName(sender.UserName, recipient.UserName);
            var group = await _unitOfWork.MessageRepository.GetMessageGroup(groupName);

            if (group.Connections.Any(x => x.UserName == recipient.UserName))
            {
                message.DateRead = DateTime.Now;
            }

            _unitOfWork.MessageRepository.AddMessage(message);
            await UpdateLastMessageChat(message);
            if (await _unitOfWork.Complete())
            {
                await Clients.Group(groupName).SendAsync("NewMessage", _mapper.Map<MessageDto>(message));
                var connections = await _presenceTracker.GetConnectionsForUser(createMessageDto.RecipientUsername);
                if (connections != null)
                {
                    var user = await _unitOfWork.UserRepository.GetMemberAsync(userName);
                    await _presenceHub.Clients.Clients(connections).SendAsync("NewMessageReceived", user, createMessageDto.Content);
                }
                var toPlayers = await _unitOfWork.OneSignalRepository.GetUserByUsername(createMessageDto.RecipientUsername);
                List<string> strTemp = new List<string>();
                foreach (var connection in toPlayers.PlayerIds)
                {
                    strTemp.Add(connection.PlayerId);
                }

                var toIds = strTemp.ToArray();

                if (toIds.Length > 0)
                {
                    var messageBody = $"😊 {sender.DisplayName.Split(' ')[0]} enviou uma mensagem para você!";
                    var obj = new
                    {
                        android_channel_id = _config["OneSignal:AndroidChannelId"],
                        app_id = _config["OneSignal:AppId"],
                        headings = new { en = "ChatApp", pt = "ChatApp" },
                        contents = new { en = messageBody, pt = messageBody },
                        include_player_ids = toIds,
                        name = "INTERNAL_CAMPAIGN_NAME"
                    };
                    await _oneSignalService.CreateNotification(obj);
                }
            }
        }

        /// <summary>
        /// Adiciona a última mensagem ao chat quando uma nova mensagem é recebida.
        /// </summary>
        /// <param name="message"></param>
        private async Task UpdateLastMessageChat(Message message)
        {
            var lastMessageFromDb = await _unitOfWork.LastMessageChatRepository.GetLastMessageChat(message.SenderUsername, message.RecipientUsername);
            if (lastMessageFromDb != null)
            {
                lastMessageFromDb.Content = message.Content;
                lastMessageFromDb.MessageLastDate = message.MessageSent;

                _unitOfWork.LastMessageChatRepository.Update(lastMessageFromDb);
            }
            else
            {
                var lastMessageChat = new LastMessageChat
                {
                    Content = message.Content,
                    MessageLastDate = message.MessageSent,
                    Sender = message.Sender,
                    Recipient = message.Recipient,
                    SenderUsername = message.SenderUsername,
                    RecipientUsername = message.RecipientUsername,
                    GroupName = GetGroupName(message.SenderUsername!, message.RecipientUsername!)
                };
                _unitOfWork.LastMessageChatRepository.Add(lastMessageChat);
            }
        }

        // Retorna o nome do grupo com base nos nomes dos usuários
        private string GetGroupName(string caller, string other)
        {
            var stringCompare = string.CompareOrdinal(caller, other) < 0;
            return stringCompare ? $"{caller}-{other}" : $"{other}-{caller}";
        }

        // Adiciona o cliente ao grupo de mensagens
        private async Task<Group> AddToGroup(string groupName)
        {
            var group = await _unitOfWork.MessageRepository.GetMessageGroup(groupName);
            var connection = new Connection(Context.ConnectionId, Context.User.Identity.Name);
            if (group == null)
            {
                group = new Group(groupName);
                _unitOfWork.MessageRepository.AddGroup(group);
            }
            group.Connections.Add(connection);

            if (await _unitOfWork.Complete()) return group;

            throw new HubException("Falha ao entrar no grupo.");
        }

        // Remove o cliente do grupo de mensagens
        private async Task<Group> RemoveFromMessageGroup()
        {
            var group = await _unitOfWork.MessageRepository.GetGroupForConnection(Context.ConnectionId);
            var connection = group.Connections.FirstOrDefault(x => x.ConnectionId == Context.ConnectionId);
            _unitOfWork.MessageRepository.RemoveConnection(connection);

            if (await _unitOfWork.Complete()) return group;

            throw new HubException("Falha ao remover do grupo.");
        }
    }
}
