using System;
using System.Threading.Tasks;
using Windows.Web.Http;

namespace Mupl.Dlna.ContentDirectory
{
    public class Service : IService
    {
        public static string ServiceId = "urn:upnp-org:serviceId:ContentDirectory";

        private static string SoapActionHeaderkey = "SOAPACTION";

        private static string SoapActionValue = "urn:schemas-upnp-org:service:ContentDirectory:1#browse";

        private Device device;

        public Service(Device device, UPnP.Service service)
        {
            this.device = device;
            ControlUrl = service.ControlURL;
        }

        public async Task<ResultType> ExecuteActionAsync<ResultType>(HttpClient httpClient, IAction<ResultType> action)
        {
            httpClient.DefaultRequestHeaders.Add(SoapActionHeaderkey, SoapActionValue);

            Uri uri;
            if (!Uri.TryCreate(new Uri(device.BaseUrl), ControlUrl, out uri))
            {
                throw new Exception("Invalid URL");
            }

            return await action.ExecuteAsync(httpClient, device, uri);
        }

        public ServiceKind ServiceKind { get { return ServiceKind.ContentDirectory; } }

        public string ControlUrl { get; set; }
    }
}
