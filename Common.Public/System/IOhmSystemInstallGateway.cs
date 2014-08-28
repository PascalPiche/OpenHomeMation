using OHM.Interfaces;
using OHM.Logger;
using OHM.Nodes;

namespace OHM.System
{
    public interface IOhmSystemInstallGateway
    {
        ILogger Logger { get; }

        bool RegisterInterface(string key);

        //void RegisterNodeType(string key, string type);
    }
}
