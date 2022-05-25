using Blazorise;
using Dignite.Cms.Admin.Sections;
using Dignite.Cms.Localization;
using Dignite.Cms.Permissions;
using Microsoft.AspNetCore.Components;
using System;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;

namespace Dignite.Cms.Admin.Blazor.Pages.Cms.Admin.Sections
{
    public partial class EditSection
    {
        [Inject] ISectionAppService SectionAppService { get; set; }
        [Inject]NavigationManager Navigation { get; set; }
        [Parameter] public Guid Id { get; set; }


        EditSectionOutput EditingEntityOutput =new();
        SectionUpdateDto EditingEntity = new();
        PageToolbar Toolbar { get; } = new();

        protected Validations CreateValidationsRef;


        public EditSection()
        {
            ObjectMapperContext = typeof(CmsAdminBlazorModule);
            LocalizationResource = typeof(CmsResource);
        }

        protected override async Task OnInitializedAsync()
        {
            await base.OnInitializedAsync();

            EditingEntityOutput = await SectionAppService.EditAsync(Id);
            EditingEntity = EditingEntityOutput.Section;
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
                requiredPolicyName: CmsPermissions.Section.Update);
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
                    await SectionAppService.UpdateAsync(Id,EditingEntity);
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
