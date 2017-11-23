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
            BaseUrl = device.URLBase;
            Services = device.Services.Select(service => ConvertService(service)).Where(x => x != null);
        }

        public IService FindService(ServiceKind serviceKind)
        {
            return Services.First(service => service.ServiceKind == serviceKind);
        }

        public static async Task<IEnumerable<Device>> SearchUPnPDeviceAsync(string deviceType, int deviceVersion)
        {
            var devices = await new Ssdp(new NetworkInfo()).SearchUPnPDevicesAsync(deviceType, deviceVersion);

            return devices.Select(device => new Device(device));
        }

        public string Name { get; private set; }

        public string Udn { get; private set; }

        public string BaseUrl { get; private set; }

        public IEnumerable<IService> Services { get; private set; }

        private IService ConvertService(UPnP.Service service)
        {
            if (service.ServiceId == ContentDirectory.Service.ServiceId)
            {
                return new ContentDirectory.Service(this, service);
            }
            else
            {
                // ignore unsupported service.
                return null;
            }
        }
    }
}
