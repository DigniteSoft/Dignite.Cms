using Blazorise;
using Dignite.SiteBuilding.Admin.Entries;
using Dignite.SiteBuilding.Localization;
using Dignite.SiteBuilding.Permissions;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;

namespace Dignite.SiteBuilding.Admin.Blazor.Pages.SiteBuilding.Admin.Entries
{
    public partial class EditEntry
    {
        [Inject] IEntryAppService EntryAppService { get; set; }
        [Inject] NavigationManager Navigation { get; set; }

        [Parameter] public Guid Id { get; set; }


        EditEntryOutput EditEntryOutput = new();
        EntryUpdateDto Entity = new();
        PageToolbar Toolbar { get; } = new();

        protected Validations ValidationsRef;


        public EditEntry()
        {
            ObjectMapperContext = typeof(SiteBuildingAdminBlazorModule);
            LocalizationResource = typeof(SiteBuildingResource);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            EditEntryOutput = await EntryAppService.EditAsync(Id);
            Entity = EditEntryOutput.Entry;
        }

        protected async override Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await base.OnAfterRenderAsync(firstRender);
                await SetToolbarItemsAsync();
            }
        }

        private ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["Save"],
                SaveAsync,
                IconName.Save,
                requiredPolicyName: SiteBuildingPermissions.Entry.Update);
            return ValueTask.CompletedTask;
        }

        private async Task SaveAsync()
        {
            try
            {
                var validate = true;
                if (ValidationsRef != null)
                {
                    validate = await ValidationsRef.ValidateAll();
                }
                if (validate)
                {
                    await EntryAppService.UpdateAsync(Id, Entity);
                    Navigation.NavigateTo($"/site-building/admin/sections/{Entity.SectionId}/entries");
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
    }
}
