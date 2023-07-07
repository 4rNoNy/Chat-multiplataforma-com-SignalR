using Microsoft.AspNetCore.Identity;

namespace ReactMXHApi6.Core.Entities
{
    public class AppUser : IdentityUser
    {
        // Propriedade para armazenar o nome de exibição do usuário
        public string DisplayName { get; set; }

        // Propriedade para armazenar a URL da imagem do usuário
        public string ImageUrl { get; set; }

        // Propriedade para armazenar a data e hora da última atividade do usuário
        public DateTime LastActive { get; set; } = DateTime.Now;

        // Coleção de posts relacionados a este usuário
        public ICollection<Post> Posts { get; set; } = new List<Post>();

        // Coleção de comentários relacionados a este usuário
        public ICollection<Comment> Comments { get; set; } = new List<Comment>();

        // Coleção de mensagens enviadas por este usuário
        public ICollection<Message> MessagesSent { get; set; }

        // Coleção de mensagens recebidas por este usuário
        public ICollection<Message> MessagesReceived { get; set; }

        // Coleção de informações sobre os últimos chats de mensagens enviados por este usuário
        public ICollection<LastMessageChat> LastMessageChatsSent { get; set; }

        // Coleção de informações sobre os últimos chats de mensagens recebidos por este usuário
        public ICollection<LastMessageChat> LastMessageChatsReceived { get; set; }

        // Coleção de identificadores de jogadores relacionados a este usuário
        public ICollection<PlayerIds> PlayerIds { get; set; } = new List<PlayerIds>();
    }
}
