using SubnetMobile.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace SubnetMobile.Helpers
{
    public class ComputationHelper
    {
        private string ConvertSubnetsToBinary(int numberOfSubnets)
        {
            return Convert.ToString(numberOfSubnets, 2);
        }

        private int GetLengthOfBinary(string binaryNum)
        {
            return binaryNum.Length;
        }

        private IpAddress InitializeSubnetMask(IpAddress ip)
        {
            IpAddress subnetMask;

            if (ip.FirstOctet >= 1 && ip.FirstOctet <= 126)
            {
                subnetMask = new IpAddress(255, 0, 0, 0);
            }
            else if (ip.FirstOctet >= 128 && ip.FirstOctet <= 191)
            {
                subnetMask = new IpAddress(255, 255, 0, 0);
            }
            else if (ip.FirstOctet >= 192 && ip.FirstOctet <= 223)
            {
                subnetMask = new IpAddress(255, 255, 255, 0);
            }
            else
            {
                subnetMask = new IpAddress();
            }

            return subnetMask;
        }

        private void UpdateSubnetMask(IpAddress ip, int bitsToBeBorrowed)
        {
            switch (bitsToBeBorrowed)
            {
                case 1:
                    ip.FourthOctet = 128;
                    break;
                case 2:
                    ip.FourthOctet = 192;
                    break;
                case 3:
                    ip.FourthOctet = 224;
                    break;
                case 4:
                    ip.FourthOctet = 240;
                    break;
                case 5:
                    ip.FourthOctet = 248;
                    break;
                case 6:
                    ip.FourthOctet = 252;
                    break;
                case 7:
                    ip.FourthOctet = 254;
                    break;
                case 8:
                    ip.FourthOctet = 255;
                    break;

                default:
                    ip.FourthOctet = 0;
                    break;
            }
        }
    }
}
