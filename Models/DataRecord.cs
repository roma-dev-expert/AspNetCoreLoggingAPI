namespace AspNetCoreLoggingAPI.Models
{
    public class DataRecord
    {
        public int Id { get; set; }       // Порядковый номер (PK, автоинкремент)
        public int Code { get; set; }     // Код
        public string Value { get; set; } // Значение
    }
}
