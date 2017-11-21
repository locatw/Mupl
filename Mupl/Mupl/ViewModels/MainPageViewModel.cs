using Mupl.Dlna;
using Mupl.Model;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Reactive.Disposables;
using System.Reactive.Linq;

namespace Mupl.ViewModels
{
    public class MainPageViewModel : BindableBase, IDisposable
    {
        private IMediaServerCollection mediaServerCollection;

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();

        public MainPageViewModel(IMediaServerCollection mediaServerCollection)
        {
            this.mediaServerCollection = mediaServerCollection;

            this.MediaServers = mediaServerCollection.MediaServers
                                    .ToReadOnlyReactiveCollection<Device>()
                                    .AddTo(this.Disposable);
            this.SelectedServer = new ReactiveProperty<Device>();
            this.SelectedServer.ObserveProperty(x => x.Value)
                .Where(x => x != null)
                .Subscribe(x =>
                {
                    System.Diagnostics.Debug.WriteLine(x.Name);
                })
                .AddTo(Disposable);

            this.mediaServerCollection.SearchAsync();
        }

        public void Dispose()
        {
            this.Disposable.Dispose();
        }

        public ReadOnlyReactiveCollection<Device> MediaServers { get; }

        public ReactiveProperty<Device> SelectedServer { get; set; }
    }
}
