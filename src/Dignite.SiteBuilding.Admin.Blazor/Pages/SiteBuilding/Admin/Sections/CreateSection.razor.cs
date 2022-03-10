using Blazorise;
using Dignite.SiteBuilding.Admin.Sections;
using Dignite.SiteBuilding.Localization;
using Dignite.SiteBuilding.Permissions;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;

namespace Dignite.SiteBuilding.Admin.Blazor.Pages.SiteBuilding.Admin.Sections
{
    public partial class CreateSection
    {
        [Inject] ISectionAppService SectionAppService { get; set; }
        [Inject]NavigationManager Navigation { get; set; }


        NewSectionOutput NewEntityOutput=new();
        SectionCreateDto NewEntity=new();
        PageToolbar Toolbar { get; } = new();

        protected Validations CreateValidationsRef;


        public CreateSection()
        {
            ObjectMapperContext = typeof(SiteBuildingAdminBlazorModule);
            LocalizationResource = typeof(SiteBuildingResource);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            NewEntityOutput = await SectionAppService.NewAsync();
            NewEntity =NewEntityOutput.Section;
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
                requiredPolicyName: SiteBuildingPermissions.Section.Create);
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
                    await SectionAppService.CreateAsync(NewEntity);
                    Navigation.NavigateTo("/site-building/admin/sections");
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
    }
}
