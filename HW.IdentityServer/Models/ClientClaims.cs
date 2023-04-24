﻿using System;
using System.Collections.Generic;

namespace HW.IdentityServerModels
{
    public partial class ClientClaims
    {
        public int Id { get; set; }
        public string Type { get; set; }
        public string Value { get; set; }
        public int ClientId { get; set; }

        public virtual Clients Client { get; set; }
    }
}
