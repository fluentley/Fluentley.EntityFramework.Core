using System;
using System.ComponentModel;

namespace Test.Fluentley.EntityFramework.Core.Entity
{
    public class Company
    {
        public Guid Id { get; set; }

        [DisplayName("Name")]
        public string Name { get; set; }

        [DisplayName("Web Site")]
        public string Website { get; set; }

        /*public bool ShouldSerializeWebsite()
        {

            return typeof(Company).GetProperty(nameof(this.Website)).Attribute<DisplayNameAttribute>() != null;
        }

        public bool ShouldSerializeName()
        {

            return typeof(Company).GetProperty(nameof(this.Name)).Attribute<DisplayNameAttribute>() != null;
        }

        public bool ShouldSerializeCommunities()
        {
            return typeof(Company).GetProperty(nameof(this.Communities)).Attribute<DisplayNameAttribute>() != null;
        }*/
    }
}