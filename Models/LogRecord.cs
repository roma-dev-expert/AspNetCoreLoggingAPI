namespace AspNetCoreLoggingAPI.Models
{
    public class LogRecord
    {
        public int Id { get; set; }
        public string Request { get; set; }
        public string Response { get; set; }
        public DateTime Timestamp { get; set; }
    }
}
