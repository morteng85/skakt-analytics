using SkaktAnalytics.Models;

namespace SkaktAnalytics.Services
{
    public class UserTableRepository : TableRepositoryBase<User>
    {
        public UserTableRepository() : base(Constants.UserTableName)
        {
        }

        public void AddIfNotExists(User user)
        {
            var exists = Exists((u) => u.UserName == user.UserName && u.Version == user.Version);

            if (!exists)
            {
                base.Add(user);
            }
        }
    }
}