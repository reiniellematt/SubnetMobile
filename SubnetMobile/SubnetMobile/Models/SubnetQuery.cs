using System;
using System.Collections.Generic;
using System.Text;

namespace SubnetMobile.Models
{
    public class SubnetQuery
    {
        /// <summary>
        /// The starting IP address the network will need to distribute.
        /// </summary>
        public CompleteIpInfo StartingIpAddress { get; set; }

        /// <summary>
        /// The number of subnets in the network.
        /// </summary>
        public int NumberOfSubnets { get; set; }
    }
}