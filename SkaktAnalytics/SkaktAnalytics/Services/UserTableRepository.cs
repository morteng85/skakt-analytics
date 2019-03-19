using SkaktAnalytics.Models;
using System;

namespace SkaktAnalytics.Services
{
    public class UserTableRepository : TableRepositoryBase<User>
    {
        public UserTableRepository() : base(Constants.UserTableName)
        {
        }

        public void AddOrUpdate(User user)
        {
            var userSameVer = GetSingle((u) => u.UserName == user.UserName && u.Version == user.Version);

            if (userSameVer != null)
            {
                userSameVer.LastUsed = DateTime.Now.ToString();

                this.Update(userSameVer);
            } else
            {
                user.LastUsed = DateTime.Now.ToString();
                user.RowKey = Guid.NewGuid().ToString();
                user.VersionInstalled = DateTime.Now.ToString();

                base.Add(user);
            }
        }
    }
}