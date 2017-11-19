using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Mupl.Model
{
    public interface IMediaServerCollection
    {
        Task SearchAsync();

        ObservableCollection<string> MediaServers { get; }
    }
}
