using System;
using System.Runtime.Serialization;

namespace Makarevich.Habr.Identity4.Models {
    [DataContract]
    [Serializable]
    public class LoginModel {
        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }

        [DataMember]
        public bool? RememberMe { get; set; } = true;

        [DataMember]
        public string ReturnUrl { get; set; }

        [DataMember]
        public string UserName { get; set; }
    }
}