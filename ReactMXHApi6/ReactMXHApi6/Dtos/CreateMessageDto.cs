namespace ReactMXHApi6.Dtos
{
    public class CreateMessageDto
    {
        // Propriedade para armazenar o nome de usuário do destinatário da mensagem
        public string RecipientUsername { get; set; }

        // Propriedade para armazenar o conteúdo da mensagem
        public string Content { get; set; }
    }
}
