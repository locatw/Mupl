namespace Mupl.Model
{
    public class MediaServer
    {
        public MediaServer(Dlna.Device device)
        {
            this.Name = device.Name;
        }

        public string Name { get; internal set; }
    }
}
