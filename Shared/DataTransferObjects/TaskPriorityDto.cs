using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    //[Serializable]
    public record TaskPriorityDto
    {
        public int Id { get; init; }
        public string? TaskTitle { get; init; }
        public string? TaskDescription { get; init; }
        public int TaskCreatedBy { get; init; }
        public DateTime? TaskToSee { get; init; }
        public DateTime CreatedDate { get; init; }
        public int? Hour { get; init; }
        public string? TaskStatus { get; init; }
        public int CategoryID { get; init; }
    }
}
