﻿
using Dignite.Abp.FieldCustomizing.FieldControls;
using Dignite.SiteBuilding.Entries;
using JetBrains.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp.MultiTenancy;

namespace Dignite.SiteBuilding.Sections
{
    /// <summary>
    /// Page Section
    /// </summary>
    public class Section : FullAuditedAggregateRoot<Guid>,IMultiTenant
    {
        protected Section()
        { }

        public Section(Guid id, string displayName, string name, string templateFile, [CanBeNull]string entryTemplateFile , Guid? tenantId)
        {
            Id = id;
            DisplayName = displayName;
            Name = name;
            TemplateFile = templateFile;
            EntryTemplateFile = entryTemplateFile;
            TenantId = tenantId;
        }


        /// <summary>
        /// TenantId of this section.
        /// </summary>
        public virtual Guid? TenantId { get; protected set; }

        /// <summary>
        /// Display Name of this section.
        /// </summary>
        [NotNull]
        public virtual string DisplayName { get; set; }

        /// <summary>
        /// Name of this section.
        /// </summary>
        [NotNull]
        public virtual string Name { get; set; }

        /// <summary>
        /// Template file of this section
        /// </summary>
        public string TemplateFile { get; set; }

        /// <summary>
        /// Template file of this entity
        /// </summary>
        public string EntryTemplateFile { get; set; }


        /// <summary>
        /// 是否为有效的条目
        /// </summary>
        public virtual bool IsActive { get; set; }


        public virtual ICollection<FieldDefinition> FieldDefinitions { get; protected set; }

        public virtual ICollection<Entry> Entries { get; protected set; }
        public virtual ICollection<SectionGrant> Authorizers { get; protected set; }


        public virtual void AddFieldDefinition(FieldDefinition field)
        {
            field.SectionId = this.Id;
            this.FieldDefinitions.Add(field);
        }

        public virtual void UpdateFieldDefinition(
            Guid fieldId,
            string displayName,
            string name,
            string defaultValue,
            string description,
            FieldControlConfigurationDictionary configuration,
            int position
            )
        {
            var fd = this.FieldDefinitions.Single(m => m.Id == fieldId);

            fd.DisplayName = displayName;
            fd.Name = name;
            fd.DefaultValue = defaultValue;
            fd.Description = description;
            fd.Configuration = configuration;
            fd.Position = position;
        }

        public virtual void DeleteFieldDefinition(Guid fieldId)
        {
            this.FieldDefinitions.RemoveAll(m => m.Id == fieldId);
        }
    }
}