using Prism.Mvvm;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

namespace Mupl.Model
{
    public class MediaServerCollection : BindableBase, IMediaServerCollection
    {
        public Task SearchAsync()
        {
            return Task.Run(async () =>
            {
                var devices = await Dlna.Device.SearchUPnPDeviceAsync("MediaServer", 1);

                devices.Select(device => device.Name).ToList().ForEach(name => MediaServers.Add(name));
            });
        }

        public ObservableCollection<string> MediaServers { get; } = new ObservableCollection<string>();
    }
}
