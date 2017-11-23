using System.Collections.Generic;
using System.Threading.Tasks;

namespace Mupl.Model
{
    public interface IMediaServerRepository
    {
        Task<IReadOnlyCollection<MediaServer>> GetAllAsync();

        Task<MediaServer> FindAsync(string udn);
    }
}
