using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mupl.Model
{
    public class MediaServerRepository : IMediaServerRepository
    {
        public async Task<IReadOnlyCollection<MediaServer>> GetAllAsync()
        {
            var devices = await Dlna.Device.SearchUPnPDeviceAsync("MediaServer", 1);

            return devices.Select(device => new MediaServer(device)).ToList();
        }
    }
}
