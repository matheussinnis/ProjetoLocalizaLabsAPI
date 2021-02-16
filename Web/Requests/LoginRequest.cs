namespace Domain.Requests
{
    public record LoginRequest
    {
        public string document { get; set; }
        public string password { get; set; }
    }
}