using Mupl.Model;
using Prism.Mvvm;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System;
using System.Reactive.Disposables;

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
                                .ToReadOnlyReactiveCollection<string>()
                                .AddTo(this.Disposable);

            this.mediaServerCollection.SearchAsync();
        }

        public void Dispose()
        {
            this.Disposable.Dispose();
        }

        public ReadOnlyReactiveCollection<string> MediaServers { get; }
    }
}
