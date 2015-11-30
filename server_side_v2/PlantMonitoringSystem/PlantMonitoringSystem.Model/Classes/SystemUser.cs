using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace PlantMonitoringSystem.Model
{
    [DataContract]
    public partial class SystemUser
    {
        [DataMember(Name = "id", EmitDefaultValue = false)]
        public int? Id { get; set; }

        [DataMember(Name = "username")]
        public string Username { get; set; }

        [DataMember(Name = "token", EmitDefaultValue = false)]
        public string AuthToken { get; set; }

        [DataMember(Name = "email", EmitDefaultValue = false)]
        public string Email { get; set; }

        [DataMember(Name = "pass", EmitDefaultValue = false)]
        public string Password { get; set; }

        public void GenerateAuthToken()
        {
            //this.AuthToken = SystemUser.Base64Encode(string.Format("__{0}__:{1}", this.Username, DateTime.Now.ToString()));
            this.AuthToken = Guid.NewGuid().ToString();
        }
    }

}
