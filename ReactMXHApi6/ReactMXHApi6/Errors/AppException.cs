namespace ReactMXHApi6.Errors
{
    public class AppException
    {
        // Construtor da classe AppException
        // Cria uma nova instância de AppException com o código de status, mensagem e detalhes fornecidos
        public AppException(int statusCode, string message, string details = null)
        {
            StatusCode = statusCode;
            Message = message;
            Details = details;
        }

        // Propriedade para armazenar o código de status da exceção
        public int StatusCode { get; set; }

        // Propriedade para armazenar a mensagem da exceção
        public string Message { get; set; }

        // Propriedade para armazenar os detalhes da exceção
        public string Details { get; set; }
    }
}
