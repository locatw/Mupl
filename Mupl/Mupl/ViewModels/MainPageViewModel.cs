using Mupl.Model;
using Prism.Mvvm;
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
        private IMediaServerRepository mediaServerRepository;

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();

        public MainPageViewModel(IMediaServerRepository mediaServerRepository)
        {
            this.mediaServerRepository = mediaServerRepository;

            this.SelectedServer = new ReactiveProperty<MediaServer>();
            this.SelectedServer.ObserveProperty(x => x.Value)
                .Where(x => x != null)
                .Subscribe(mediaServer =>
                {
                    System.Diagnostics.Debug.WriteLine(mediaServer.Name);
                })
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

        public ReactiveCollection<MediaServer> MediaServers { get; private set; } = new ReactiveCollection<MediaServer>();

        public ReactiveProperty<MediaServer> SelectedServer { get; set; }
    }
}
