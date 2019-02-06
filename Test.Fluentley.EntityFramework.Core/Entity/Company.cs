using System;
using System.ComponentModel;

namespace Test.Fluentley.EntityFramework.Core.Entity
{
    public class Company
    {
        public Guid Id { get; set; }

        [DisplayName("Name")] public string Name { get; set; }

        [DisplayName("Web Site")] public string Website { get; set; }
    }
}