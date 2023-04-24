using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HW.GatewayApi.Helpers
{
  public class ServiceConfiguration
  {
    public AWSS3Configuration AWSS3 { get; set; }
  }
  public class AWSS3Configuration
  {
    public string BucketName { get; set; }
  }
}
