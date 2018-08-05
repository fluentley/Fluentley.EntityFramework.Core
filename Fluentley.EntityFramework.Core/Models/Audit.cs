using System;

namespace Fluentley.EntityFramework.Core.Models
{
    public class Audit
    {
        public string TableName { get; set; }
        public DateTime DateTime { get; set; }

        public string KeyValues { get; set; }
        public string OldValues { get; set; }
        public string NewValues { get; set; }
    }
}