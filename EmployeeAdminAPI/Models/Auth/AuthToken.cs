namespace LoginAdminWebAPI.Models.Auth
{
    public class AuthToken
    {
        public string UserName { get; set; }
        public string Token { get; set; }
        public DateTime ExpirationDate { get; set; }
    }
}
