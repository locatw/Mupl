using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mupl.Model
{
    public class MediaServerRepository : IMediaServerRepository
    {
        private List<MediaServer> mediaServers;

        public async Task<IReadOnlyCollection<MediaServer>> GetAllAsync()
        {
            if (mediaServers == null)
            {
                await LoadAsync();
            }

            return mediaServers;
        }

        public async Task<MediaServer> FindAsync(string udn)
        {
            if (mediaServers == null)
            {
                await LoadAsync();
            }

            return mediaServers.Find(server => server.Udn == udn);
        }

        private async Task LoadAsync()
        {
            var devices = await Dlna.Device.SearchUPnPDeviceAsync("MediaServer", 1);

            mediaServers = devices.Select(device => new MediaServer(device)).ToList();
        }
    }
}
