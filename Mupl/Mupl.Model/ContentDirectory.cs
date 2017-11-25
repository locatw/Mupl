using System.Collections.ObjectModel;
using System.Linq;

namespace Mupl.Model
{
    public class ContentDirectory : IDirectoryItem
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
            var dirItems = await mediaServer.LoadDirectoryItemsAsync(Id);

            DirectoryItems.Clear();
            dirItems.ToList().ForEach(dirItem => DirectoryItems.Add(dirItem));
        }

        public string Id { get; private set; }

        public string Name { get; private set; }

        public ObservableCollection<IDirectoryItem> DirectoryItems { get; } = new ObservableCollection<IDirectoryItem>();
    }
}
