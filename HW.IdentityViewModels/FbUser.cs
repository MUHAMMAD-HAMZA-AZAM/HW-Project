using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace HW.IdentityViewModels
{
    public class FbUser
    {
        public string Id { get; set; }

        public string Email { get; set; }

        [JsonProperty("first_name")]
        public string FirstName { get; set; }

        [JsonProperty("last_name")]
        public string LastName { get; set; }
        public FbUserPicture Picture { get; set; }
    }

    public class FbUserPicture
    {
        public FbUserPictureData Data { get; set; }
    }

    public class FbUserPictureData
    {
        public string Url { get; set; }
    }
}
