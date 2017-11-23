using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UPnP;

namespace Mupl.Dlna
{
    public class Device
    {
        public Device(UPnP.Device device)
        {
            Name = device.FriendlyName;
            Udn = device.UDN;
        }

        public static async Task<IEnumerable<Device>> SearchUPnPDeviceAsync(string deviceType, int deviceVersion)
        {
            var devices = await new Ssdp(new NetworkInfo()).SearchUPnPDevicesAsync(deviceType, deviceVersion);

            return devices.Select(device => new Device(device));
        }

        public string Name { get; private set; }

        public string Udn { get; private set; }
    }
}
