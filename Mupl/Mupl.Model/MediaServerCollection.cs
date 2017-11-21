using Mupl.Dlna;
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
                var devices = await Device.SearchUPnPDeviceAsync("MediaServer", 1);

                devices.ToList().ForEach(device => MediaServers.Add(device));
            });
        }

        public ObservableCollection<Device> MediaServers { get; } = new ObservableCollection<Device>();
    }
}
