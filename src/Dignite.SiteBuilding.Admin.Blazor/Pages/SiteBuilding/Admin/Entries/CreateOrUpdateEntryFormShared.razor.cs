using Blazorise;
using Dignite.Abp.FieldCustomizing.Blazor;
using Dignite.SiteBuilding.Admin.Entries;
using Dignite.SiteBuilding.Admin.Pages;
using Dignite.SiteBuilding.Localization;
using Dignite.SiteBuilding.Pages;
using Dignite.SiteBuilding.Sections;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.Localization;
using System;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.AspNetCore.Components.Web;

namespace Dignite.SiteBuilding.Admin.Blazor.Pages.SiteBuilding.Admin.Entries
{

    public partial class CreateOrUpdateEntryFormShared
    {
        [Inject] private IFieldControlComponentSelector fieldControlComponentSelector { get; set; }
        [Inject] private IPageAppService PageAppService { get; set; }

        [Parameter] public EntryCreateOrUpdateDtoBase Entry { get; set; }
        [Parameter] public SectionDto Section { get; set; }


        [Parameter] public AbpBlazorMessageLocalizerHelper<SiteBuildingResource> LH { get; set; }
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
