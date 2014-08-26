using OHM.Interfaces;
using OHM.Logger;
using OHM.Nodes;

namespace OHM.System
{
    public interface IOhmSystemInstallGateway
    {
        ILogger Logger { get; }

        void RegisterInterface(IInterface newInterface);

        void RegisterNodeType(INode obj);
    }
}
