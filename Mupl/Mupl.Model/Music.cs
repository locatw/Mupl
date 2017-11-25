namespace Mupl.Model
{
    public class Music : IDirectoryItem
    {
        private MediaServer mediaServer;

        public Music(MediaServer mediaServer, Dlna.ContentDirectory.Item item)
        {
            this.mediaServer = mediaServer;
            Id = item.ID;
            Name = item.Title;
        }

        public string Id { get; private set; }

        public string Name { get; private set; }
    }
}
