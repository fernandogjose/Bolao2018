namespace Core.Domain.Models
{
    public class ChatCreateRequest
    {
        public string UserId { get; set; }

        public string Message { get; set; }
    }
}