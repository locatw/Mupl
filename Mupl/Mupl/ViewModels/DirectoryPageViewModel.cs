using Mupl.Model;
using Prism.Windows.Mvvm;
using Prism.Windows.Navigation;
using Reactive.Bindings;
using Reactive.Bindings.Extensions;
using System.Reactive.Disposables;
using System.Collections.Generic;

namespace Mupl.ViewModels
{
    public class DirectoryPageViewModel : ViewModelBase
    {
        private IMediaServerRepository mediaServerRepository;

        private MediaServer mediaServer;

        public DirectoryPageViewModel(IMediaServerRepository mediaServerRepository)
        {
            this.mediaServerRepository = mediaServerRepository;
        }

        public override async void OnNavigatedTo(NavigatedToEventArgs e, Dictionary<string, object> viewModelState)
        {
            base.OnNavigatedTo(e, viewModelState);

            var id = (string)e.Parameter;
            mediaServer = await mediaServerRepository.FindAsync(id);

            if (mediaServer == null)
            {
                return;
            }

            ContentDirectories = mediaServer.ContentDirectories
                                    .ToReadOnlyReactiveCollection()
                                    .AddTo(Disposable);

            mediaServer.LoadContentDirectoriesAsync();
        }

        public ReadOnlyReactiveCollection<ContentDirectory> ContentDirectories { get; private set; }

        public ReactiveProperty<ContentDirectory> SelectedContentDirectory { get; set; }

        private CompositeDisposable Disposable { get; } = new CompositeDisposable();
    }
}