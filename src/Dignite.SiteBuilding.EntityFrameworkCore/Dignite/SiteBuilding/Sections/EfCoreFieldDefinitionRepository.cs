using Dignite.SiteBuilding.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.SiteBuilding.Sections
{
    public class EfCoreFieldDefinitionRepository : EfCoreRepository<ISiteBuildingDbContext, FieldDefinition, Guid>, IFieldDefinitionRepository
    {

        public EfCoreFieldDefinitionRepository(
            IDbContextProvider<ISiteBuildingDbContext> dbContextProvider
            )
            : base(dbContextProvider)
        {
        }
    }
}
