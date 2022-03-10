using Blazorise;
using Dignite.SiteBuilding.Localization;
using Dignite.SiteBuilding.Sections;
using Dignite.SiteBuilding.Permissions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.EntityActions;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Microsoft.AspNetCore.Components;

namespace Dignite.SiteBuilding.Admin.Blazor.Pages.SiteBuilding.Admin.Sections
{
    public partial class Sections
    {
        [Inject]
        public NavigationManager Navigation { get; set; }

        protected PageToolbar Toolbar { get; } = new();

        protected List<TableColumn> SectionManagementTableColumns => TableColumns.Get<Sections>();

        public Sections()
        {
            ObjectMapperContext = typeof(SiteBuildingAdminBlazorModule);
            LocalizationResource = typeof(SiteBuildingResource);

            CreatePolicyName = SiteBuildingPermissions.Section.Create;
            UpdatePolicyName = SiteBuildingPermissions.Section.Update;
            DeletePolicyName = SiteBuildingPermissions.Section.Delete;
        }

        protected override ValueTask SetEntityActionsAsync()
        {
            EntityActions
                .Get<Sections>()
                .AddRange(new EntityAction[]
                {
                    new EntityAction
                    {
                        Text = L["Edit"],
                        Visible = (data) => HasUpdatePermission,
                        Clicked = async (data) => { Navigation.NavigateTo($"/site-building/admin/sections/{((SectionDto)data).Id}/edit"); }
                    },
                    new EntityAction
                    {
                        Text = L["Delete"],
                        Visible = (data) => HasDeletePermission,
                        Clicked = async (data) => await DeleteEntityAsync(data.As<SectionDto>()),
                        ConfirmationMessage = (data) => GetDeleteConfirmationMessage(data.As<SectionDto>())
                    }
                });

            return base.SetEntityActionsAsync();
        }

        protected override ValueTask SetTableColumnsAsync()
        {
            SectionManagementTableColumns
                .AddRange(new TableColumn[]
                {
                    new TableColumn
                    {
                        Title = L["Actions"],
                        Actions = EntityActions.Get<Sections>()
                    },
                    new TableColumn
                    {
                        Title = L["DisplayName"],
                        Data = nameof(SectionDto.DisplayName),
                        Component = typeof(SectionDisplayNameComponent)
                    },
                    new TableColumn
                    {
                        Title = L["SectionName"],
                        Data = nameof(SectionDto.Name)
                    },
                });

            return base.SetTableColumnsAsync();
        }


        protected override string GetDeleteConfirmationMessage(SectionDto entity)
        {
            return string.Format(L["SectionDeletionConfirmationMessage"], entity.Name);
        }

        protected override ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["NewSection"],
                async ()=>  Navigation.NavigateTo("/site-building/admin/sections/create"),
                IconName.Add,
                requiredPolicyName: CreatePolicyName);

            return base.SetToolbarItemsAsync();
        }
    }
}
