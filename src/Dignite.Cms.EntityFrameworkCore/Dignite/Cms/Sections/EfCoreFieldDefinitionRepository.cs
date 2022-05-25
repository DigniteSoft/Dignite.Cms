using Dignite.Cms.EntityFrameworkCore;
using System;
using Volo.Abp.Domain.Repositories.EntityFrameworkCore;
using Volo.Abp.EntityFrameworkCore;

namespace Dignite.Cms.Sections
{
    public class EfCoreFieldDefinitionRepository : EfCoreRepository<ICmsDbContext, FieldDefinition, Guid>, IFieldDefinitionRepository
    {

        public EfCoreFieldDefinitionRepository(
            IDbContextProvider<ICmsDbContext> dbContextProvider
            )
            : base(dbContextProvider)
        {
        }
    }
}
