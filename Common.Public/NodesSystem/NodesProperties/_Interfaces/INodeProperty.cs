using System;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace OHM.Nodes.Properties
{
    /// <summary>
    /// Node property interface with minimal property and methods.
    /// It's the core interface for all property in the system.
    /// </summary>
    public interface INodeProperty : INotifyPropertyChanged
    {
        #region Properties

        /// <summary>
        /// Unique Key of the node property.
        /// Can not be null.
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Name of the node property.
        /// Can not be null.
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Type used for the property value
        /// </summary>
        Type Type { get; }

        /// <summary>
        /// Flag defining read only node property
        /// </summary>
        /// <value>True when value is read only</value>
        bool ReadOnly { get; }

        /// <summary>
        /// Short description of the node property
        /// </summary>
        /// <value>Return string or String.empty</value>
        string Description { get; }

        /// <summary>
        /// Actual Value of the property
        /// </summary>
        object Value { get; }

        //todo
        /// <summary>
        /// Get the sub properties collection
        /// </summary>
        ReadOnlyCollection<INodeProperty> Properties
        {
            get;
        }

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
