using System.Threading.Tasks;
using Shouldly;
using Xunit;

namespace Dignite.Cms.Pages
{
    public class PageAppService_Tests : CmsApplicationTestBase
    {
        private readonly IPageAppService _pageAppService;

        public PageAppService_Tests()
        {
            _pageAppService = GetRequiredService<IPageAppService>();
        }

        [Fact]
        public async Task GetAsync()
        {
            var result = await _pageAppService.GetListAsync();
            result.Items.Count.ShouldBe(42);
        }

    }
}
