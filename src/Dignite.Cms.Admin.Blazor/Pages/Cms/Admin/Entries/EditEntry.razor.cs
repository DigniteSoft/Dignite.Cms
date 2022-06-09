using Blazorise;
using Dignite.Cms.Admin.Entries;
using Dignite.Cms.Localization;
using Dignite.Cms.Permissions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.Content;

namespace Dignite.Cms.Admin.Blazor.Pages.Cms.Admin.Entries
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
            ObjectMapperContext = typeof(CmsAdminBlazorModule);
            LocalizationResource = typeof(CmsResource);
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
                requiredPolicyName: CmsPermissions.Entry.Update);
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
                    //如果含有文件，需要读取文件流
                    foreach (var field in Entity.CustomizedFields)
                    {
                        if (field.Value!=null && field.Value.GetType() == typeof(List<IFileEntry>))
                        {
                            var remoteStreamContents = new List<IRemoteStreamContent>();
                            foreach (var file in (List<IFileEntry>)field.Value)
                            {
                                remoteStreamContents.Add(
                                    new RemoteStreamContent(
                                        file.OpenReadStream(long.MaxValue),
                                        file.Name,
                                        file.Type
                                        ));
                            }
                            Entity.CustomizedFieldFiles.Add(field.Key, remoteStreamContents);
                            Entity.CustomizedFields[field.Key] = null;
                        }
                    }

                    await EntryAppService.UpdateAsync(Id, Entity);
                    Navigation.NavigateTo($"/cms/admin/sections/{Entity.SectionId}/entries");
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
    }
}
