using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UPnP;

namespace Mupl.Dlna
{
    public class Device
    {
        public static async Task<IEnumerable<Device>> SearchUPnPDeviceAsync(string deviceType, int deviceVersion)
        {
            var devices = await new Ssdp(new NetworkInfo()).SearchUPnPDevicesAsync(deviceType, deviceVersion);

            return devices.Select(device => new Device() { Name = device.FriendlyName });
        }

        public string Name { get; internal set; }
    }
}
