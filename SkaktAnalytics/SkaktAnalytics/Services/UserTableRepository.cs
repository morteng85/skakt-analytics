using SkaktAnalytics.Models;

namespace SkaktAnalytics.Services
{
    public class UserTableRepository : TableRepositoryBase<User>
    {
        public UserTableRepository() : base(Constants.UserTableName)
        {
        }

        public void AddOrUpdate(User user)
        {
            var exists = Exists((u) => u.UserName == user.UserName);

            if (!exists)
            {
                base.Add(user);
            }

            bool update = false;
            var currUser = GetSingle((u) => u.UserName == user.UserName);
            
            if (currUser.Version != user.Version) {
                update = true;
            }

            if (currUser.Theme != user.Theme)
            {
                update = true;
            }

            if (currUser.Lines != user.Lines)
            {
                update = true;
            }

            if (currUser.Highlight != user.Highlight)
            {
                update = true;
            }

            if (update)
            {
                this.Update(currUser, user);
            }
        }
    }
}