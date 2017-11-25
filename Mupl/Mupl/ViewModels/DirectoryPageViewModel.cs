using Mupl.Model;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Collections.Generic;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using Windows.UI.Xaml.Navigation;

namespace Mupl.ViewModels
{
    public class DirectoryPageViewModel : ViewModelBase, IDisposable
    {
        private INavigationService navigationService;

        private IMediaServerRepository mediaServerRepository;

        private MediaServer mediaServer;

        private IContentDirectory contentDirectory;

        public DirectoryPageViewModel(INavigationService navigationService, IMediaServerRepository mediaServerRepository)
        {
            this.navigationService = navigationService;
            this.mediaServerRepository = mediaServerRepository;

            SelectedDirectoryItem = new ReactiveProperty<IDirectoryItem>();
            SelectedDirectoryItem.ObserveProperty(x => x.Value)
                .Where(x => x is ContentDirectory)
                .Cast<ContentDirectory>()
                .Subscribe(dir => MoveToContentDirectoryPage(dir))
                .AddTo(Disposable);
        }

        public void Dispose()
        {
            Disposable.Dispose();
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            var parameters = NavigationParameters.CreateFromString((string)e.Parameter);
            string mediaServerId = parameters.MediaServerId;

            mediaServer = await mediaServerRepository.FindAsync(mediaServerId);
            if (mediaServer == null)
            {
                return;
            }

            if (parameters.HasParentDirectoryId)
            {
                contentDirectory = mediaServer.GetContentDirectory(parameters.ParentDirectoryId);
            }
            else
            {
                contentDirectory = mediaServer;
            }

            if (contentDirectory == null)
            {
                return;
            }

            DirectoryItems = contentDirectory.DirectoryItems
                                .ToReadOnlyReactiveCollection()
                                .AddTo(Disposable);

            if (e.NavigationMode == NavigationMode.New)
            {
                await contentDirectory.LoadDirectoryItemsAsync();
            }
        }

        public ReadOnlyReactiveCollection<IDirectoryItem> DirectoryItems { get; private set; }

        public ReactiveProperty<IDirectoryItem> SelectedDirectoryItem { get; set; }

        private void MoveToContentDirectoryPage(ContentDirectory selectedContentDirectory)
        {
            var parameters = new NavigationParameters(mediaServer.Id, selectedContentDirectory.Id);

            navigationService.Navigate(PageTokens.Directory.ToString(), parameters.ToString());
        }

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();
    }
}