using Dignite.Abp.FieldCustomizing;
using Dignite.Abp.FieldCustomizing.FieldControls;
using JetBrains.Annotations;
using System;
using Volo.Abp;
using Volo.Abp.Auditing;
using Volo.Abp.Domain.Entities;
using Volo.Abp.MultiTenancy;

namespace Dignite.SiteBuilding.Sections
{
    public class FieldDefinition : BasicAggregateRoot<Guid>, IMultiTenant, ICustomizeFieldDefinition, IDeletionAuditedObject, IHasDeletionTime, ISoftDelete
    {
        public FieldDefinition(
            Guid id,  
            string displayName, 
            string name, 
            string defaultValue,
            FieldControlConfigurationDictionary configuration,  
            Guid? tenantId)
        {
            Id = id;
            DisplayName = displayName;
            Name = name;
            DefaultValue = defaultValue;
            Configuration = configuration;
            TenantId = tenantId;
        }

        /// <summary>
        /// TenantId of this field.
        /// </summary>
        public virtual Guid? TenantId { get; protected set; }

        public virtual Guid SectionId { get; set; }


        /// <summary>
        /// Display name of this field.
        /// </summary>
        [NotNull]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// Unique Name
        /// </summary>
        [NotNull]
        public virtual string Name { get; set; }


        /// <summary>
        /// Description
        /// </summary>
        [CanBeNull]
        public string Description { get; set; }

        /// <summary>
        /// Default value of the field.
        /// </summary>
        [CanBeNull]
        public string DefaultValue { get; set; }

        [NotNull]
        public string FieldControlProviderName { get; }

        [NotNull]
        public virtual FieldControlConfigurationDictionary Configuration { get; set; }

        public int Position { get; set; }

        public virtual bool IsDeleted { get; set; }
        public virtual Guid? DeleterId { get; set; }
        public virtual DateTime? DeletionTime { get; set; }
    }
}
