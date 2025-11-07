namespace Application.Utils
{
    public class EmailOptions
    {
        public string Host { get; set; } = string.Empty;
        public int Port { get; set; } = 25;
        public string From { get; set; } = string.Empty;
        public string? DisplayName { get; set; }
        public string? UserName { get; set; }
        public string? Password { get; set; }
        public bool EnableSsl { get; set; } = true;
    }
}
