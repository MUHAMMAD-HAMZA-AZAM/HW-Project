using System;
using System.Collections.Generic;

namespace HW.IdentityServerModels
{
    public partial class ClientIdPrestrictions
    {
        public int Id { get; set; }
        public string Provider { get; set; }
        public int ClientId { get; set; }

        public virtual Clients Client { get; set; }
    }
}
