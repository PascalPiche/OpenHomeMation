using OHM.Logger;

namespace OHM.RAL
{
    public abstract class InterfaceAbstract : NodeInterfaceAbstract
    {

        #region Public Ctor

        public InterfaceAbstract(string key, string name, ILogger logger) 
            : base(key, name, logger)
        {}

        #endregion
    }
}
