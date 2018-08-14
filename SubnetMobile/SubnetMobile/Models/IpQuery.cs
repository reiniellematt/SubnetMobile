using System;
using System.Collections.Generic;
using System.Text;

namespace SubnetMobile.Models
{
    public class IpQuery
    {
        public int NumberOfSubnets { get; set; }
        public IpAddress StartingIpAddress { get; set; }
    }
}