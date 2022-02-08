using Microsoft.AspNetCore.Authorization.Infrastructure;

namespace Dignite.SiteBuilding
{
    public static class CommonOperations
    {
        public static OperationAuthorizationRequirement Read = new OperationAuthorizationRequirement { Name = nameof(Read) };
    }
}
