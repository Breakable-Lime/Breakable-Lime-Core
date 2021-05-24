namespace BreakableLime.ExternalModels.Requests
{
    public record CreateUserRequest
    {
        public string Email;
        public string Password;

        public string Class;

        public bool IsAdmin;
    }
}