using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Data.Entities.Base;
using Data.Enums;

namespace Data.Entities
{
    public class Log : Entity
    {
        public string EventPublishName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public TypeEvent TypeEvent { get; set; }
        public DateTime Date { get; set; }
    }
}
