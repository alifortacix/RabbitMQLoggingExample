namespace RabbitMQ.Entities
{
    public class Log
    {
        public LogTypes LogType { get; set; }
        public string Message { get; set; }
    }


    public enum LogTypes
    {
        System,
        Warning,
        Error,
        Success,
    }
}
