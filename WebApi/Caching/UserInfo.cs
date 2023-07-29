namespace WebApi.Caching
{
    public class UserInfo
    {
        public string ConnectionId { get; set; } = string.Empty;
        public string UserId { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;
        public string RoleType { get; set; } = string.Empty;

        public bool IsCustomer => this.RoleType == "CUSTOMER";
        public bool IsSeller => this.RoleType == "SELLER";
    }
}
