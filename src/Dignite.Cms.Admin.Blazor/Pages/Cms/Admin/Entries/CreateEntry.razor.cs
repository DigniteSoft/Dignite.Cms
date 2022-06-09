using Blazorise;
using Dignite.Cms.Admin.Entries;
using Dignite.Cms.Localization;
using Dignite.Cms.Permissions;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using Volo.Abp.AspNetCore.Components.Web.Theming.PageToolbars;
using Volo.Abp.Content;

namespace Dignite.Cms.Admin.Blazor.Pages.Cms.Admin.Entries
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
            ObjectMapperContext = typeof(CmsAdminBlazorModule);
            LocalizationResource = typeof(CmsResource);
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
                IconName.Save);
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
                    //如果含有文件，需要读取文件流
                    foreach (var field in NewEntity.CustomizedFields)
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
                            NewEntity.CustomizedFieldFiles.Add(field.Key, remoteStreamContents);
                            NewEntity.CustomizedFields[field.Key] = null;
                        }
                    }

                    //
                    await EntryAppService.CreateAsync(NewEntity);
                    Navigation.NavigateTo($"/cms/admin/sections/{sectionId}/entries");
                }
            }
            catch (Exception ex)
            {
                await HandleErrorAsync(ex);
            }
        }
    }
}
