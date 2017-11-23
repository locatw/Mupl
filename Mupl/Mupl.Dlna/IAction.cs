using System;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace Mupl.Dlna
{
    public interface IAction<ResultType>
    {
        Task<ResultType> ExecuteAsync(HttpClient httpClient, Device device, Uri uri);
    }
}
