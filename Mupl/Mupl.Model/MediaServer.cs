using Prism.Mvvm;
using System.Collections.ObjectModel;

namespace Mupl.Model
{
    public class MediaServer : BindableBase
    {
        public MediaServer(Dlna.Device device)
        {
            this.Name = device.Name;
            this.Udn = device.Udn;
        }

        public async void LoadContentDirectoriesAsync()
        {
            await System.Threading.Tasks.Task.Delay(2000);

            ContentDirectories.Add(new ContentDirectory() { Name = "Directory1" });
            ContentDirectories.Add(new ContentDirectory() { Name = "Directory2" });
        }

        public string Name { get; private set; }

        public string Udn { get; private set; }

        public ObservableCollection<ContentDirectory> ContentDirectories { get; } = new ObservableCollection<ContentDirectory>();
    }
}
