using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContextEf
{
    public class DataBaseSettings
    {
        public required string FilePath { get; set; } = "AlfaLogger.db";
        public override string ToString() => FilePath;
    }
}
