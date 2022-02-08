using System;
using Volo.Abp.Domain.Repositories;

namespace Dignite.SiteBuilding.Sections
{
    public interface IFieldDefinitionRepository : IBasicRepository<FieldDefinition, Guid>
    {
    }
}
