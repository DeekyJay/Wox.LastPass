using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Wox.LastPass
{
    [DataContract]
    public class LastPassEntry
    {
        [DataMember(Name = "id")]
        public string Id { get; set; }

        [DataMember(Name = "name")]
        public string Name { get; set; }

        [DataMember(Name = "fullname")]
        public string FullName { get; set; }

        [DataMember(Name = "username")]
        public string UserName { get; set; }

        [DataMember(Name = "password")]
        public string Password { get; set; }

        [DataMember(Name = "last_modified_gmt")]
        private string LastModifiedGmtString;

        public DateTime LastModifiedGmt
        {
            get
            {
                var i = int.Parse(this.LastModifiedGmtString);
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(i);
            }
        }

        [DataMember(Name = "last_touch")]
        public string LastTouchString;

        public DateTime LastTouch
        {
            get
            {
                var i = int.Parse(this.LastTouchString);
                return new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(i);
            }
        }

        [DataMember(Name = "group")]
        public string Group { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        [DataMember(Name = "note")]
        public string Note { get; set; }
    }
}
