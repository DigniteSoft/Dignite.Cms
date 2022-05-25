using System;
using Volo.Abp.Domain.Repositories;

namespace Dignite.Cms.Sections
{
    public interface IFieldDefinitionRepository : IBasicRepository<FieldDefinition, Guid>
    {
    }
}
