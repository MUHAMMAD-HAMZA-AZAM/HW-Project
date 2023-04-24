using System;
using System.Collections.Generic;
using System.Text;

namespace HW.Utility
{
    public class SigninKeyCredentials
    {
        public string KeyFilePath { get; set; }
        public string KeyFilePassword { get; set; }
    }

    public class ServerDown
    {
        public bool IsServerDown { get; set; }
        public string Message { get; set; }
    }

    public class Announcement
    {
        public bool HasAnnouncement { get; set; }
        public string Message { get; set; }
    }

    public class UpdateRequired
    {
        public long VersionCode { get; set; }
        public bool Flexable { get; set; }
    }

    public class BoolKeyValue
    {
        public bool Key { get; set; }
        public bool Flexable { get; set; }
        public string Value { get; set; }
    }
}
