namespace Mupl
{
    class NavigationParameters
    {
        public NavigationParameters(string mediaServerId, string parenttDirectoryId = null)
        {
            MediaServerId = mediaServerId;
            ParentDirectoryId = parenttDirectoryId;
        }

        public override string ToString()
        {
            var parentDirectoryId = ParentDirectoryId != null ? ParentDirectoryId : "";

            return $"{MediaServerId}_{parentDirectoryId}";
        }

        public static NavigationParameters CreateFromString(string value)
        {
            var elems = value.Split('_');

            string mediaServerId = elems[0];
            string parentDirectoryId = string.IsNullOrEmpty(elems[1]) ? null : elems[1];

            return new NavigationParameters(mediaServerId, parentDirectoryId);
        }

        public string MediaServerId { get; private set; }

        public string ParentDirectoryId { get; private set; }

        public bool HasParentDirectoryId { get { return ParentDirectoryId != null; } }
    }
}
