using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Enums;

namespace Repository.DtoObjects
{
    public record LoggingEventDto(
        string EventPublishName,
        string Message,
        TypeEvent TypeEvent,
        DateTime Date);
}
