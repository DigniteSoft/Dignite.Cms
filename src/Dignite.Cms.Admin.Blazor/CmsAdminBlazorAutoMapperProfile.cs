using AutoMapper;
using Dignite.Cms.Admin.Pages;
using Dignite.Cms.Pages;
using Dignite.Cms.Admin.Sections;
using Dignite.Cms.Sections;

namespace Dignite.Cms.Admin.Blazor
{
    public class CmsAdminBlazorAutoMapperProfile : Profile
    {
        public CmsAdminBlazorAutoMapperProfile()
        {
            CreateMap<PageDto, PageCreateDto>()
                .MapExtraProperties();

            CreateMap<PageDto, PageUpdateDto>()
                .MapExtraProperties();
        }
    }
}