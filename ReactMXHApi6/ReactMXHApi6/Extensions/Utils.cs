namespace ReactMXHApi6.Extensions
{
    public static class Utils
    {
        // Converte uma data e hora em um valor inteiro do tipo UInt32
        public static uint ToDoUInt32DateTime(this DateTime dateTime)
        {
            DateTime startTime = new DateTime(1970, 1, 1, 0, 0, 0, 0);
            TimeSpan currTime = dateTime - startTime;
            uint time_t = Convert.ToUInt32(Math.Abs(currTime.TotalSeconds));
            return time_t;
        }
    }
}
