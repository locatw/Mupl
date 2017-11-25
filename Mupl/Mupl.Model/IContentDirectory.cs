using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Mupl.Model
{
    public interface IContentDirectory
    {
        Task LoadDirectoryItemsAsync();

        ObservableCollection<IDirectoryItem> DirectoryItems { get; }
    }
}
