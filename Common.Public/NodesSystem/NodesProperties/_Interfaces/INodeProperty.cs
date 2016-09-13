using System;

namespace OHM.Nodes.Properties
{
    /// <summary>
    /// 
    /// </summary>
    public interface INodeProperty
    {
        #region Properties

        /// <summary>
        /// 
        /// </summary>
        string Key { get; }

        /// <summary>
        /// 
        /// </summary>
        string Name { get; }

        /// <summary>
        /// 
        /// </summary>
        string Description { get; }

        /// <summary>
        /// 
        /// </summary>
        bool ReadOnly { get; }

        /// <summary>
        /// 
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// 
        /// </summary>
        object Value { get; }

        #endregion

        #region API

        /// <summary>
        /// 
        /// </summary>
        /// <param name="val"></param>
        /// <returns></returns>
        bool SetValue(object val);

        #endregion

    }
}
