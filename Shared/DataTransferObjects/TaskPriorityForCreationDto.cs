using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shared.DataTransferObjects
{
    public record TaskPriorityForCreationDto(
        string TaskTitle,
        string TaskDescription,
        int TaskCreatedBy,
        DateTime? TaskToSee,
        DateTime? CreatedDate,
        int? Hour,
        string TaskStatus
    );
}
