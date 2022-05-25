
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace Dignite.Cms
{
    public static class CmsEntityFrameworkCoreQueryableExtensions
    {
        public static IQueryable<Sections.Section> IncludeDetails(this IQueryable<Sections.Section> queryable, bool include = true)
        {
            if (!include)
            {
                return queryable;
            }

            return queryable
                .Include(x => x.FieldDefinitions);
        }
    }
}
