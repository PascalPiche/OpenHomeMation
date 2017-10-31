using OHM.Logger;

namespace OHM.SYS
{
    /// <summary>
    /// 
    /// </summary>
    public interface IOhmSystemUnInstallGateway
    {
        /// <summary>
        /// 
        /// </summary>
        ILogger Logger { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool UnRegisterInterface(string key);
    }
}
