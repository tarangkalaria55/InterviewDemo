using System.Collections.Concurrent;

namespace WebApi.Caching
{
    public class UserInfoInMemory : IUserInfoInMemory
    {
        private readonly ConcurrentDictionary<string, UserInfo> _onlineUser = new();

        public bool AddUpdate(string userid, string username, string roletype, string connectionId)
        {
            var key = GetNameKey(userid, username, roletype);
            if (!string.IsNullOrEmpty(userid) && !string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(roletype))
            {
                var userAlreadyExists = _onlineUser.ContainsKey(key);

                var userInfo = new UserInfo
                {
                    UserId = userid,
                    UserName = username,
                    RoleType = roletype,
                    ConnectionId = connectionId
                };

                _onlineUser.AddOrUpdate(key, userInfo, (key, value) => userInfo);

                return userAlreadyExists;
            }

            throw new ArgumentNullException(nameof(key));
        }

        public void Remove(string? key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                _onlineUser.TryRemove(key, out _);
            }
        }

        public IEnumerable<UserInfo> GetAllUsersExceptThis(string? key)
        {
            if (string.IsNullOrEmpty(key))
                return new List<UserInfo>();

            return _onlineUser.Values.Where(item => item.UserName != key);
        }

        public UserInfo? GetUserInfo(string? key)
        {
            if (!string.IsNullOrEmpty(key))
            {
                _onlineUser.TryGetValue(key, out UserInfo? userInfo);
                return userInfo;
            }

            return null;
        }

        public string GetNameKey(string userid, string username, string role)
        {
            return $"{role}__{userid}__{username}";
        }
    }
}
