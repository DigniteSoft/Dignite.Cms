using Blazorise;
using Dignite.Cms.Admin.Sections;
using Dignite.Cms.Localization;
using Dignite.Cms.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;

namespace Dignite.Cms.Admin.Blazor.Pages.Cms.Admin.Sections
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
            ObjectMapperContext = typeof(CmsAdminBlazorModule);
            LocalizationResource = typeof(CmsResource);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            NewEntityOutput = await SectionAppService.NewAsync();
            NewEntity =NewEntityOutput.Section;
            await SetToolbarItemsAsync();
        }


        private ValueTask SetToolbarItemsAsync()
        {
            Toolbar.AddButton(L["Save"],
                SaveAsync,
                IconName.Save,
                requiredPolicyName: CmsPermissions.Section.Create);
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
                    Navigation.NavigateTo("/cms/admin/sections");
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
    }
}
