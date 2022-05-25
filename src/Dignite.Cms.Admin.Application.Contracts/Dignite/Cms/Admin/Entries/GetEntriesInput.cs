
using Dignite.Cms.Entries;
using System;
using System.ComponentModel.DataAnnotations;
using Volo.Abp.Application.Dtos;

namespace Dignite.Cms.Admin.Entries
{
    public class GetEntriesInput : PagedResultRequestDto
    {
        public GetEntriesInput()
        {
            MaxResultCount = 50;
        }


        [Required]
        public Guid SectionId { get; set; }

        public Guid? PageId { get; set; }

        //public string Filter { get; set; }

        public EntryAuditStatus? AuditedStatus { get; set; }

        public bool? IsActive { get; set; }

        public Guid? CreatorId { get; set; }
    }
}
