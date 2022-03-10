using System;
using Microsoft.EntityFrameworkCore;
using Volo.Abp;
using Volo.Abp.EntityFrameworkCore.Modeling;
using Dignite.Abp.FieldCustomizing.EntityFrameworkCore.Modeling;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Dignite.SiteBuilding.Users;
using Volo.Abp.Users.EntityFrameworkCore;
using JetBrains.Annotations;

namespace Dignite.SiteBuilding.EntityFrameworkCore
{
    public static class SiteBuildingDbContextModelCreatingExtensions
    {
        public static void ConfigureSiteBuilding(
            [NotNull]this ModelBuilder builder)
        {
            Check.NotNull(builder, nameof(builder));


            builder.Entity<SiteUser>(b =>
            {
                b.ToTable(SiteBuildingDbProperties.DbTablePrefix + "Users", SiteBuildingDbProperties.DbSchema);

                b.ConfigureByConvention();
                b.ConfigureAbpUser();

                b.ApplyObjectExtensionMappings();
            });

            builder.Entity<Pages.Page>(b =>
            {
                //Configure table & schema name
                b.ToTable(SiteBuildingDbProperties.DbTablePrefix + "Pages", SiteBuildingDbProperties.DbSchema);

                b.ConfigureByConvention();
            
                //Properties
                b.Property(q => q.Title).IsRequired().HasMaxLength(Pages.PageConsts.MaxTitleLength);
                b.Property(q => q.Path).IsRequired().HasMaxLength(Pages.PageConsts.MaxPathLength);
                b.Property(q => q.Description).HasMaxLength(Pages.PageConsts.MaxDescriptionLength);
                b.Property(q => q.TemplateFile).HasMaxLength(Pages.PageConsts.MaxTemplateFileLength);
                b.Property(q => q.PermissionName).HasMaxLength(Pages.PageConsts.MaxPermissionNameLength);
                b.Property(q => q.Keywords).HasConversion(
                    config => JsonConvert.SerializeObject(config, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }),
                    jsonData => JsonConvert.DeserializeObject<string[]>(jsonData)
                    );

                //Relations
                b.HasMany(et => et.Entries).WithOne().HasForeignKey(et => et.PageId);

                //Indexs
                b.HasIndex(q => q.Path);
            });

            builder.Entity<Sections.Section>(b =>
            {
                //Configure table & schema name
                b.ToTable(SiteBuildingDbProperties.DbTablePrefix + "Sections", SiteBuildingDbProperties.DbSchema);

                b.ConfigureByConvention();

                //Properties
                b.Property(q => q.DisplayName).IsRequired().HasMaxLength(Sections.SectionConsts.MaxDisplayNameLength);
                b.Property(q => q.Name).IsRequired().HasMaxLength(Sections.SectionConsts.MaxNameLength);
                b.Property(q => q.TemplateFile).HasMaxLength(Pages.PageConsts.MaxTemplateFileLength);
                b.Property(q => q.EntryTemplateFile).HasMaxLength(Pages.PageConsts.MaxTemplateFileLength);

                //Relations
                b.HasMany(et => et.FieldDefinitions).WithOne().HasForeignKey(et => et.SectionId);
                b.HasMany(et => et.Entries).WithOne().HasForeignKey(et => et.SectionId);
                b.HasMany(et => et.Authorizers).WithOne().HasForeignKey(et => et.SectionId);
            });

            builder.Entity<Sections.SectionGrant>(b =>
            {
                //Configure table & schema name
                b.ToTable(SiteBuildingDbProperties.DbTablePrefix + "SectionGrants", SiteBuildingDbProperties.DbSchema);

                b.ConfigureByConvention();

                //Properties
                b.Property(q => q.SectionId).IsRequired();
                b.Property(q => q.UserId).IsRequired();
                b.Property(q => q.PageIds).HasConversion(
                    config => JsonConvert.SerializeObject(config, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() }),
                    jsonData => JsonConvert.DeserializeObject<Guid[]>(jsonData)
                    );

                //Key
                b.HasKey(auth => new
                {
                    auth.SectionId,
                    auth.UserId
                });
            });

            builder.Entity<Sections.FieldDefinition>(b =>
            {
                //Configure table & schema name
                b.ToTable(SiteBuildingDbProperties.DbTablePrefix + "FieldDefinitions", SiteBuildingDbProperties.DbSchema);

                b.ConfigureByConvention();
                b.ConfigureCustomizableFieldDefinitions();

                //Properties
                b.Property(q => q.DisplayName).IsRequired().HasMaxLength(Sections.FieldDefinitionConsts.MaxDisplayNameLength);
                b.Property(q => q.Name).IsRequired().HasMaxLength(Sections.FieldDefinitionConsts.MaxNameLength);
                b.Property(q => q.FieldControlProviderName).IsRequired().HasMaxLength(Sections.FieldDefinitionConsts.MaxFieldControlProviderNameLength);

                //Indexes
                b.HasIndex(q => q.SectionId);
            });

            builder.Entity<Entries.Entry>(b =>
            {
                //Configure table & schema name
                b.ToTable(SiteBuildingDbProperties.DbTablePrefix + "Entries", SiteBuildingDbProperties.DbSchema);

                b.ConfigureByConvention();
                b.ConfigureObjectCustomizedFields();

                b.Property(q => q.Slug).HasMaxLength(Entries.EntryConsts.MaxSlugLength);

                //Indexes
                b.HasIndex(q => new {q.SectionId, q.PageId, q.CreationTime });
                b.HasIndex(q => new { q.SectionId, q.CreatorId, q.CreationTime });
            });

            builder.TryConfigureObjectExtensions<SiteBuildingDbContext>();
        }
    }
}