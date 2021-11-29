namespace AspMVC_Monitor.Model
{
    public class TcpExchangeInit
    {
        public const int TcpPort = 9590;
        public const int TcpMessagebByteSize = 1024 * 1024;

        public bool Init { get; set; }

        public TcpExchangeInit(bool init)
        {
            this.Init = init;
        }
    }
}
