using System;
using System.Collections.Generic;
using System.Text;

namespace SubnetMobile.Models
{
    public class CompleteIpInfo
    {
        /// <summary>
        /// The main IP address.
        /// </summary>
        public IpAddress MainIp { get; set; }

        /// <summary>
        /// The subnet mask.
        /// </summary>
        public IpAddress SubnetMask { get; set; }

        /// <summary>
        /// The prefix length of the IP address.
        /// </summary>
        public int PrefixLength { get; set; }

        /// <summary>
        /// Creates the full IP information of a given IP address.
        /// </summary>
        /// <param name="ip">The IP address.</param>
        public CompleteIpInfo(IpAddress ip)
        {
            MainIp = ip;

            if (ip.FirstOctet >= 1 && ip.FirstOctet <= 126)
            {
                SubnetMask = new IpAddress(255, 0, 0, 0);
                PrefixLength = 8;
            }
            else if (ip.FirstOctet >= 128 && ip.FirstOctet <= 191)
            {
                SubnetMask = new IpAddress(255, 255, 0, 0);
                PrefixLength = 16;
            }
            else if (ip.FirstOctet >= 192 && ip.FirstOctet <= 223)
            {
                SubnetMask = new IpAddress(255, 255, 255, 0);
                PrefixLength = 24;
            }
            else
            {
                SubnetMask = new IpAddress();
                PrefixLength = 0;
            }
        }
    }
}
