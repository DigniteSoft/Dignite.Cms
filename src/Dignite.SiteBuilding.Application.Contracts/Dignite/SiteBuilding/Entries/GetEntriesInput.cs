
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Dignite.SiteBuilding.Entries
{
    public class GetEntriesInput : PagedResultRequestDto
    {
        public GetEntriesInput()
        {
            MaxResultCount = 15;
        }

        [Required]
        public Guid PageId { get; set; }

        [Required]
        public Guid SectionId { get; set; }

        public Guid? CreatorId { get; set; }
    }
}
