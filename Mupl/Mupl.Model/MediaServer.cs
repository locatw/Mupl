using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace Mupl.Model
{
    public class MediaServer : BindableBase
    {
        private Dlna.Device device;

        private Dictionary<string, ContentDirectory> contentDirectoryCache = new Dictionary<string, ContentDirectory>();

        public MediaServer(Dlna.Device device)
        {
            this.device = device;
        }

        public ContentDirectory GetContentDirectory(string objectId)
        {
            return contentDirectoryCache.ContainsKey(objectId) ? contentDirectoryCache[objectId] : null;
        }

        public async void LoadContentDirectoriesAsync()
        {
            var directories = await LoadContentDirectoriesAsync(Dlna.ContentDirectory.BrowseAction.RootObjectId);

            directories.ToList().ForEach(dir => ContentDirectories.Add(dir));
        }

        public string Id { get { return device.Udn; } }

        public string Name { get { return device.Name; } }

        public ObservableCollection<ContentDirectory> ContentDirectories { get; } = new ObservableCollection<ContentDirectory>();

        internal async Task<IEnumerable<ContentDirectory>> LoadContentDirectoriesAsync(string objectId)
        {
            var contentDirectoryService = device.FindService(Dlna.ServiceKind.ContentDirectory);
            if (contentDirectoryService == null)
            {
                return new List<ContentDirectory>();
            }

            var httpClient = new HttpClient();

            var browseAction = CreateBrowseAction(objectId);
            var browseActionResult = await contentDirectoryService.ExecuteActionAsync(httpClient, browseAction);
            var directories = browseActionResult.Body.BrowseResponse.Result.Elements
                                .Where(elem => elem is Dlna.ContentDirectory.Container)
                                .Cast<Dlna.ContentDirectory.Container>()
                                .Select(container => new ContentDirectory(this, container));

            CacheLoadedContentDirectories(directories);

            return directories;
        }

        private Dlna.ContentDirectory.BrowseAction CreateBrowseAction(string objectId)
        {
            return new Dlna.ContentDirectory.BrowseAction
            {
                ObjectId = objectId,
                BrowseFlag = Dlna.ContentDirectory.BrowseFlag.BrowseDirectChildren,
                Filter = Dlna.Filter.All,
                StartingIndex = 0,
                RequestedCount = Dlna.ContentDirectory.BrowseAction.RequestAll
            };
        }

        private void CacheLoadedContentDirectories(IEnumerable<ContentDirectory> loadedContentDirectories)
        {
            foreach (var dir in loadedContentDirectories.Where(dir => !contentDirectoryCache.ContainsKey(dir.Id)))
            {
                contentDirectoryCache[dir.Id] = dir;
            }
        }

        private string BaseUrl { get { return device.BaseUrl; } }

        private IEnumerable<Dlna.IService> Services { get { return device.Services; } }
    }
}
