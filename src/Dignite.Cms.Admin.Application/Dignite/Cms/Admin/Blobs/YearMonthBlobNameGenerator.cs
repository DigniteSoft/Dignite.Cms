using Dignite.Abp.BlobStoring;
using System;
using System.Threading.Tasks;
using Volo.Abp.DependencyInjection;


namespace Dignite.Cms.Admin.Blobs
{
    public class YearMonthBlobNameGenerator: IBlobNameGenerator, ITransientDependency
    {  
        public Task<string> Create(string extensionName = null)
        {
            return Task.FromResult(
                DateTime.Now.Year.ToString() + "/" + DateTime.Now.Month.ToString()+"/"+ Guid.NewGuid().ToString("N") + extensionName.EnsureStartsWith('.')
                );
        }
    }
}
