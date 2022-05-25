using Volo.Abp.Uow;
using Volo.Abp.Users;

namespace Dignite.Cms.Users
{
    public class SiteUserLookupService : UserLookupService<SiteUser, ISiteUserRepository>, ISiteUserLookupService
    {
        public SiteUserLookupService(
            ISiteUserRepository userRepository,
            IUnitOfWorkManager unitOfWorkManager)
            : base(
                userRepository,
                unitOfWorkManager)
        {
            
        }

        protected override SiteUser CreateUser(IUserData externalUser)
        {
            return new SiteUser(externalUser);
        }
    }
}