using WebApi.Common.Interfaces;

namespace WebApi.Caching
{
    public interface IUserInfoInMemory : ITransientService
    {
        bool AddUpdate(string userid, string username, string roletype, string connectionId);

        void Remove(string? key);

        IEnumerable<UserInfo> GetAllUsersExceptThis(string? key);

        UserInfo? GetUserInfo(string? key);

        string GetNameKey(string userid, string username, string role);

        IEnumerable<UserInfo> GetAllUsers();
    }
}
