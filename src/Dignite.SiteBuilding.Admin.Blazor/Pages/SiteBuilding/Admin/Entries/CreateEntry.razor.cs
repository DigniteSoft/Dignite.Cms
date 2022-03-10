using Blazorise;
using Dignite.SiteBuilding.Admin.Entries;
using Dignite.SiteBuilding.Localization;
using Dignite.SiteBuilding.Permissions;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using System.Web;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;

namespace Dignite.SiteBuilding.Admin.Blazor.Pages.SiteBuilding.Admin.Entries
{
    public partial class CreateEntry
    {
        [Inject] IEntryAppService EntryAppService { get; set; }
        [Inject] NavigationManager Navigation { get; set; }


        NewEntryOutput NewEntityOutput = new();
        EntryCreateDto NewEntity = new();
        PageToolbar Toolbar { get; } = new();

        protected Validations CreateValidationsRef;

        Guid sectionId = Guid.Empty;

        public CreateEntry()
        {
            ObjectMapperContext = typeof(SiteBuildingAdminBlazorModule);
            LocalizationResource = typeof(SiteBuildingResource);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            var uri = Navigation.ToAbsoluteUri(Navigation.Uri);
            sectionId = Guid.Parse( HttpUtility.ParseQueryString(uri.Query).Get("SectionId"));
            NewEntityOutput = await EntryAppService.NewAsync(sectionId);
            NewEntity = NewEntityOutput.Entry;
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
                requiredPolicyName: SiteBuildingPermissions.Entry.Create);
            return ValueTask.CompletedTask;
        }

        private async Task SaveAsync()
        {
            try
            {
                var validate = true;
                if (CreateValidationsRef != null)
                {
                    validate = await CreateValidationsRef.ValidateAll();
                }
                if (validate)
                {
                    await EntryAppService.CreateAsync(NewEntity);
                    Navigation.NavigateTo($"/site-building/admin/sections/{sectionId}/entries");
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
    }
}
