namespace ReactMXHApi6
{
    // Representa um objeto de previs�o do tempo
    public class WeatherForecast
    {
        // Data da previs�o
        public DateTime Date { get; set; }

        // Temperatura em graus Celsius
        public int TemperatureC { get; set; }

        // Temperatura em graus Fahrenheit (calculada automaticamente a partir da temperatura em Celsius)
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        // Resumo da previs�o (opcional)
        public string? Summary { get; set; }
    }
}
