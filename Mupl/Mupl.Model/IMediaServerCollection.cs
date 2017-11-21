using Mupl.Dlna;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace Mupl.Model
{
    public interface IMediaServerCollection
    {
        Task SearchAsync();

        ObservableCollection<Device> MediaServers { get; }
    }
}
