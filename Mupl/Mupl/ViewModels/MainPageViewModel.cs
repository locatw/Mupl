using Mupl.Model;
using Prism.Mvvm;
using Prism.Windows.Navigation;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Mupl.ViewModels
{
    public class MainPageViewModel : BindableBase, IDisposable
    {
        private INavigationService navigationService;

        private IMediaServerRepository mediaServerRepository;

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();

        public MainPageViewModel(INavigationService navigationService, IMediaServerRepository mediaServerRepository)
        {
            this.navigationService = navigationService;
            this.mediaServerRepository = mediaServerRepository;

            this.SelectedServer = new ReactiveProperty<MediaServer>();
            this.SelectedServer.ObserveProperty(x => x.Value)
                .Where(x => x != null)
                .Subscribe(mediaServer => MoveToContentDirectoryPage(mediaServer))
                .AddTo(Disposable);

            Initialize();
        }

        public void Dispose()
        {
            this.Disposable.Dispose();
        }

        private async void Initialize()
        {
            var mediaServers = await mediaServerRepository.GetAllAsync();
            mediaServers.ToList().ForEach(x => MediaServers.Add(x));
        }

        private void MoveToContentDirectoryPage(MediaServer selectedMediaServer)
        {
            navigationService.Navigate(PageTokens.Directory.ToString(), selectedMediaServer.Id);
        }

        public ReactiveCollection<MediaServer> MediaServers { get; private set; } = new ReactiveCollection<MediaServer>();

        public ReactiveProperty<MediaServer> SelectedServer { get; set; }
    }
}
