namespace ReactMXHApi6
{
    // Representa um objeto de previsão do tempo
    public class WeatherForecast
    {
        // Data da previsão
        public DateTime Date { get; set; }

        // Temperatura em graus Celsius
        public int TemperatureC { get; set; }

        // Temperatura em graus Fahrenheit (calculada automaticamente a partir da temperatura em Celsius)
        public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

        // Resumo da previsão (opcional)
        public string? Summary { get; set; }
    }
}
