using AutoMapper;
using Dignite.SiteBuilding.Admin.Pages;
using Dignite.SiteBuilding.Pages;

namespace Dignite.SiteBuilding.Admin.Blazor
{
    public class SiteBuildingAdminBlazorAutoMapperProfile : Profile
    {
        public SiteBuildingAdminBlazorAutoMapperProfile()
        {
            CreateMap<PageDto, PageCreateDto>()
                .MapExtraProperties();

            CreateMap<PageDto, PageUpdateDto>()
                .MapExtraProperties();
        }
    }
}