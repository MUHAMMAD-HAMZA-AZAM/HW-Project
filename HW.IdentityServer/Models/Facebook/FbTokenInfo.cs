using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.IdentityServer.Models.Facebook
{
    public class FbTokenInfo
    {
        public FbTokenInfoData Data { get; set; }        
    }

    public class FbTokenInfoData
    {
        [JsonProperty("app_id")]
        public string AppId { get; set; }
        public string Type { get; set; }
        public string Application { get; set; }

        [JsonProperty("data_access_expires_at")]
        public int DataAccessExpiresAt { get; set; }

        [JsonProperty("expires_at")]
        public int ExpiresAt { get; set; }

        [JsonProperty("is_valid")]
        public bool IsValid { get; set; }

        [JsonProperty("issued_at")]
        public int IssuedAt { get; set; }
        public List<string> Scopes { get; set; }

        [JsonProperty("user_id")]
        public string UserId { get; set; }
    }
}
