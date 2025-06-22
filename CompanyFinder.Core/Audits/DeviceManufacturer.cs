using System.Net.NetworkInformation;

namespace CompanyFinder.Core.Audits
{
    public static class DeviceManufacturer
    {
        private static readonly Dictionary<string, string> OuiLookup = new Dictionary<string, string>
        {
            { "00:14:22", "Dell" },
            { "00:50:56", "VMware" },
            { "00:1A:11", "Lenovo" },
            { "00:25:00", "Apple" },
            { "00:A0:60", "Acer"},
            { "08:BF:B8", "Asus"},
            { "E8:B0:C5", "Monster"},
            { "EA:B0:C5", "Monster"},
            { "00:30:37", "Packard Bell"},
            { "00:20:09", "Packard Bell"},
            { "00:16:17", "MSI"},
            { "00:1F:CF", "MSI"},
            { "38:BC:01", "Huawei"},
            { "34:1E:6B", "Huawei"},
            { "88:66:39", "Huawei"},
            { "14:9D:09", "Huawei"},
            { "4C:F9:5D", "Huawei"},
            { "84:21:F1", "Huawei"},
            { "70:79:90", "Huawei"}
            // For More (IEEE list: https://standards-oui.ieee.org/)
        };
        public static string GetMACAddress()
        {
            try
            {
                NetworkInterface[] nics = NetworkInterface.GetAllNetworkInterfaces();
                foreach (NetworkInterface adapter in nics)
                {
                    if (adapter.OperationalStatus == OperationalStatus.Up &&
                        adapter.NetworkInterfaceType != NetworkInterfaceType.Loopback &&
                        adapter.NetworkInterfaceType != NetworkInterfaceType.Tunnel)
                    {
                        string macAddress = adapter.GetPhysicalAddress().ToString();
                        if (!string.IsNullOrEmpty(macAddress))
                        {
                            return string.Join(":", Enumerable.Range(0, macAddress.Length / 2)
                                .Select(i => macAddress.Substring(i * 2, 2)));
                        }
                    }
                }
            }
            catch (Exception)
            {
                return "Unknown MAC Address";
            }
            return "Unknown MAC Address";
        }
        public static string GetManufacturerFromMac(string macAddress)
        {
            if (string.IsNullOrEmpty(macAddress) || macAddress.Length < 8)
            {
                return "Unknown Manufacturer";
            }
            macAddress = macAddress.Replace("-", ":").ToUpper();
            if (macAddress.Length == 12 && !macAddress.Contains(":"))
            {
                macAddress = string.Join(":", Enumerable.Range(0, macAddress.Length / 2)
                    .Select(i => macAddress.Substring(i * 2, 2)));
            }
            var oui = macAddress.Substring(0, 8);
            return OuiLookup.TryGetValue(oui, out var manufacturer) ? manufacturer : "Unknown Manufacturer";
        }
    }
}
