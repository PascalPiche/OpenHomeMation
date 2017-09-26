using System;

namespace OHM.Nodes.Properties
{
    /// <summary>
    /// Node property interface with minimal property and methods.
    /// It's the core interface for all property in the system.
    /// </summary>
    public interface INodeProperty
    {
        #region Properties

        /// <summary>
        /// Unique Key for the property
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Name of the property
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Short description of the property
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Flag defining read only property
        /// </summary>
        bool ReadOnly { get; }

        /// <summary>
        /// Type used for the property value
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Actual Value of the property
        /// </summary>
        object Value { get; }

        #endregion

        #region API

        /// <summary>
        /// Function to set the value
        /// </summary>
        /// <param name="val">New value to set</param>
        /// <returns>Return true if succeed elsewhere return false</returns>
        bool SetValue(object val);

        #endregion
    }
}
