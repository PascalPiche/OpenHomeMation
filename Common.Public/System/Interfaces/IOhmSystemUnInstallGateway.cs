using OHM.Logger;

namespace OHM.SYS
{
    public interface IOhmSystemUnInstallGateway
    {
        ILogger Logger { get; }

        bool UnRegisterInterface(string key);

    }
}
