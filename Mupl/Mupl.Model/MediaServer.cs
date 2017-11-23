using Prism.Mvvm;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Windows.Web.Http;

namespace Mupl.Model
{
    public class MediaServer : BindableBase
    {
        private Dlna.Device device;

        public MediaServer(Dlna.Device device)
        {
            this.device = device;
        }

        public async void LoadContentDirectoriesAsync()
        {
            var contentDirectoryService = device.FindService(Dlna.ServiceKind.ContentDirectory);
            if (contentDirectoryService == null)
            {
                return;
            }

            var httpClient = new HttpClient();

            var browseAction = CreateBrowseAction();
            var browseActionResult = await contentDirectoryService.ExecuteActionAsync(httpClient, browseAction);

            browseActionResult.Body.BrowseResponse.Result.Elements
                .Where(elem => elem is Dlna.ContentDirectory.Container)
                .Select(elem => new ContentDirectory() { Name = elem.Title })
                .ToList()
                .ForEach(dir => ContentDirectories.Add(dir));
        }

        public string Id { get { return device.Udn; } }

        public string Name { get { return device.Name; } }

        public ObservableCollection<ContentDirectory> ContentDirectories { get; } = new ObservableCollection<ContentDirectory>();

        private Dlna.ContentDirectory.BrowseAction CreateBrowseAction()
        {
            return new Dlna.ContentDirectory.BrowseAction
            {
                ObjectId = Dlna.ContentDirectory.BrowseAction.RootObjectId,
                BrowseFlag = Dlna.ContentDirectory.BrowseFlag.BrowseDirectChildren,
                Filter = Dlna.Filter.All,
                StartingIndex = 0,
                RequestedCount = Dlna.ContentDirectory.BrowseAction.RequestAll
            };
        }

        private string BaseUrl { get { return device.BaseUrl; } }

        private IEnumerable<Dlna.IService> Services { get { return device.Services; } }
    }
}
