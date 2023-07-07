using System.Text.Json;

namespace ReactMXHApi6.Extensions
{
    public static class HttpExtensions
    {
        // Adiciona cabeçalho de paginação à resposta HTTP
        public static void AddPaginationHeader(this HttpResponse response, int currentPage, int itemsPerPage, int totalItems, int totalPages)
        {
            // Cria um objeto contendo as informações de paginação
            var paginationHeader = new
            {
                currentPage,
                itemsPerPage,
                totalItems,
                totalPages
            };

            // Serializa o objeto para JSON e adiciona o cabeçalho "Pagination" à resposta
            response.Headers.Add("Pagination", JsonSerializer.Serialize(paginationHeader));

            // Adiciona o cabeçalho "Access-Control-Expose-Headers" para expor o cabeçalho "Pagination"
            response.Headers.Add("Access-Control-Expose-Headers", "Pagination");
        }
    }
}
