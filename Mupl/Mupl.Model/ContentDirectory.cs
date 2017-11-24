using System.Collections.ObjectModel;
using System.Linq;

namespace Mupl.Model
{
    public class ContentDirectory
    {
        private MediaServer mediaServer;

        public ContentDirectory(MediaServer mediaServer, Dlna.ContentDirectory.Container container)
        {
            this.mediaServer = mediaServer;
            Id = container.ID;
            Name = container.Title;
        }

        public async void LoadContentDirectoriesAsync()
        {
            var directories = await mediaServer.LoadContentDirectoriesAsync(Id);

            directories.ToList().ForEach(dir => ContentDirectories.Add(dir));
        }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public ObservableCollection<ContentDirectory> ContentDirectories { get; } = new ObservableCollection<ContentDirectory>();
    }
}
