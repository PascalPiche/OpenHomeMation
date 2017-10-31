using System.Collections.Generic;

namespace OHM.Nodes.Commands
{
    /// <summary>
    /// Minimal command definition property
    /// </summary>
    public interface ICommandDefinition
    {
        /// <summary>
        /// Unique key of the command
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Name of the command
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Short description of the command
        /// </summary>
        string Description { get; }

        /// <summary>
        /// Dictionnary of all arguments definition for the command
        /// </summary>
        IDictionary<string, IArgumentDefinition> ArgumentsDefinition { get; }
    }
}
