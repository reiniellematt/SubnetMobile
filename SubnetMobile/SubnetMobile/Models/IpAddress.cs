using System;
using System.Collections.Generic;
using System.Text;

namespace SubnetMobile.Models
{
    /// <summary>
    /// A class that represents an IP Address.
    /// </summary>
    public class IpAddress
    {
        /// <summary>
        /// The first octet or the IP Address.
        /// </summary>
        public short FirstOctet { get; set; }

        /// <summary>
        /// The second octet of the IP Address.
        /// </summary>
        public short SecondOctet { get; set; }

        /// <summary>
        /// The third octet of the IP Address.
        /// </summary>
        public short ThirdOctet { get; set; }

        /// <summary>
        /// The fourth octet of the IP Address.
        /// </summary>
        public short FourthOctet { get; set; }

        public override string ToString()
        {
            return $"{ FirstOctet }.{ SecondOctet }.{ ThirdOctet }.{ FourthOctet }";
        }
    }
}
