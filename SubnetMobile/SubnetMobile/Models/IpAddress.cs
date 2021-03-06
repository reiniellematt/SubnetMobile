﻿using System;
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
        public int FirstOctet { get; set; }

        /// <summary>
        /// The second octet of the IP Address.
        /// </summary>
        public int SecondOctet { get; set; }

        /// <summary>
        /// The third octet of the IP Address.
        /// </summary>
        public int ThirdOctet { get; set; }

        /// <summary>
        /// The fourth octet of the IP Address.
        /// </summary>
        public int FourthOctet { get; set; }

        /// <summary>
        /// Initializes an empty IP address.
        /// </summary>
        public IpAddress() { }

        /// <summary>
        /// Creates an IP address with its octets or values.
        /// </summary>
        public IpAddress(int firstOctet, int secondOctet, int thirdOctet, int fourthOctet)
        {
            FirstOctet = firstOctet;
            SecondOctet = secondOctet;
            ThirdOctet = thirdOctet;
            FourthOctet = fourthOctet;
        }

        public string DisplayName
        {
            get
            {
                return $"{FirstOctet}.{SecondOctet}.{ThirdOctet}.{FourthOctet}";
            }
        }
    }
}
