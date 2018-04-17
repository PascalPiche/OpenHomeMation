using log4net;

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
        ILog Logger { get; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool UnRegisterInterface(string key);
    }
}
