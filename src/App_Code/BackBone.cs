using System;
using System.Collections.Generic;
using System.Web;
using System.IO;

namespace ITCommunity {
    public struct IpRange {
        public uint Ip;
        public uint Mask;
    }
    public static class BackBone {
        public static bool IsBackboneUser() {
            uint ip = getIpFromString(HttpContext.Current.Request.UserHostAddress);
            bool isBackBone = false;
            foreach (IpRange range in GetBackBoneAddressesFromCache()) {
                if ((ip & range.Mask) == range.Ip) {
                    isBackBone = true;
                    break;
                }
            }
            return isBackBone;
        }

        internal static List<IpRange> GetBackBoneAddressesFromCache() {
            var list = AppCache.Get("BackBoneAddresses", _backBoneListLoader, new object[] { }, 1);
            return (List<IpRange>)list;
        }
        private delegate List<IpRange> BackBoneListLoader();

        private static BackBoneListLoader _backBoneListLoader = GetBackBoneAddresses;

        private static List<IpRange> GetBackBoneAddresses() {
            string[] ips = File.ReadAllLines(HttpContext.Current.Request.PhysicalApplicationPath + @"\media\other\bbn.txt");
            List<IpRange> addresses = new List<IpRange>(20);
            uint ip;
            foreach (string s in ips) {
                if (!s.Contains(";")) {
                    ip = getIpFromString(s.Trim().Substring(0, s.IndexOf("/")));
                    if (ip > 0) {
                        IpRange address = new IpRange();
                        address.Ip = ip;
                        byte prefix = Convert.ToByte(s.Substring(s.IndexOf("/") + 1));
                        address.Mask = uint.MaxValue << 32 - prefix;
                        addresses.Add(address);
                    }
                }
            }
            return addresses;
        }
        private static uint getIpFromString(string ip) {
            string[] ipBytes = ip.Split(new char[] { '.' });
            uint intIp;
            if (ipBytes.Length == 4) {
                intIp = Convert.ToUInt32(ipBytes[0]) * 256 * 256 * 256 + Convert.ToUInt32(ipBytes[1]) * 256 * 256 + Convert.ToUInt32(ipBytes[2]) * 256 + Convert.ToUInt32(ipBytes[3]);
            } else {
                intIp = 0;
            }
            return intIp;
        }
    }
}