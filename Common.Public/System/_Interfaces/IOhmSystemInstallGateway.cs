using log4net;

namespace OHM.SYS
{
    public interface IOhmSystemInstallGateway
    {
        ILog Logger { get; }

        bool RegisterInterface(string key);

        bool RegisterVrType(string key);
    }
}
