using Blazorise;
using Dignite.Abp.FieldCustomizing.Blazor;
using Dignite.Cms.Admin.Entries;
using Dignite.Cms.Admin.Pages;
using Dignite.Cms.Localization;
using Dignite.Cms.Pages;
using Dignite.Cms.Sections;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web;

namespace Dignite.Cms.Admin.Blazor.Pages.Cms.Admin.Entries
{

    public partial class CreateOrUpdateEntryFormShared
    {
        [Inject] private IFieldControlComponentSelector fieldControlComponentSelector { get; set; }
        [Inject] private IPageAppService PageAppService { get; set; }

        [Parameter] public EntryCreateOrUpdateDtoBase Entry { get; set; }
        [Parameter] public SectionDto Section { get; set; }


        [Parameter] public AbpBlazorMessageLocalizerHelper<CmsResource> LH { get; set; }
        [Parameter] public IStringLocalizer L { get; set; }

        ListResultDto<PageDto> allPages = new();


        protected override async Task OnInitializedAsync()
        {
            allPages = await PageAppService.GetAllListAsync();

            await base.OnInitializedAsync();
        }


        public void OnPageListSelected(string value)
        {
            Entry.PageId=Guid.Parse(value);
        }

    }
}
