using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Exceptions
{
    public sealed class MaxHourRangeBadRequestException : BadRequestException
    {
        public MaxHourRangeBadRequestException() : base("Max Hour can't be lesse tha min hour")
        {
        }
    }
}
