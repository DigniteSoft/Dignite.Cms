using Blazorise;
using Dignite.Cms.Localization;
using Dignite.Cms.Entries;
using Dignite.Cms.Permissions;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.EntityActions;
using Volo.Abp.AspNetCore.Components.Web.Extensibility.TableColumns;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Microsoft.AspNetCore.Components;

namespace Dignite.Cms.Admin.Blazor.Pages.Cms.Admin.Sections
{
    public partial class Entries
    {
        [Inject]
        public NavigationManager Navigation { get; set; }

        protected PageToolbar Toolbar { get; } = new();

        protected List<TableColumn> EntryManagementTableColumns => TableColumns.Get<Entries>();
        [Parameter] public Guid Id { get; set; }

        public Entries()
        {
            ObjectMapperContext = typeof(CmsAdminBlazorModule);
            LocalizationResource = typeof(CmsResource);

            CreatePolicyName = CmsPermissions.Entry.Create;
            UpdatePolicyName = CmsPermissions.Entry.Update;
            DeletePolicyName = CmsPermissions.Entry.Delete;
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
                        Clicked = async (data) => { Navigation.NavigateTo($"/cms/admin/entries/{((EntryDto)data).Id}/edit"); }
                    },
                    new EntityAction
                    {
                        Text = L["Delete"],
                        Visible = (data) => HasDeletePermission,
                        Clicked = async (data) => await DeleteEntityAsync(data.As<EntryDto>()),
                        ConfirmationMessage = (data) => GetDeleteConfirmationMessage(data.As<EntryDto>())
                    }
                });

            return base.SetEntityActionsAsync();
        }

        protected override ValueTask SetTableColumnsAsync()
        {
            EntryManagementTableColumns
                .AddRange(new TableColumn[]
                {
                    new TableColumn
                    {
                        Title = L["CreationTime"],
                        Data = nameof(EntryDto.CreationTime),
                        DisplayFormat="{0:yyyy-MM-dd}"
                    },
                    new TableColumn
                    {
                        Title = L["PublishTime"],
                        Data = nameof(EntryDto.PublishTime),
                        DisplayFormat="{0:yyyy-MM-dd}"
                    },
                    new TableColumn
                    {
                        Title = L["Actions"],
                        Actions = EntityActions.Get<Sections>()
                    }
                });

            return base.SetTableColumnsAsync();
        }


        protected override string GetDeleteConfirmationMessage(EntryDto entity)
        {
            return string.Format(L["EntryDeletionConfirmationMessage"]);
        }

        protected override ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["NewEntry"],
                async () => Navigation.NavigateTo($"/cms/admin/entries/create?sectionId={Id}"),
                IconName.Add,
                requiredPolicyName: CreatePolicyName);

            return base.SetToolbarItemsAsync();
        }

        protected override Task UpdateGetListInputAsync()
        {
            base.UpdateGetListInputAsync();

            GetListInput.SectionId = Id;

            return Task.CompletedTask;
        }
    }
}
