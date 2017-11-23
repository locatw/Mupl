using System.Threading.Tasks;
using Windows.Web.Http;

namespace Mupl.Dlna
{
    public interface IService
    {
        Task<ResultType> ExecuteActionAsync<ResultType>(HttpClient httpClient, IAction<ResultType> action);

        ServiceKind ServiceKind { get; }

        string ControlUrl { get; }
    }
}
