using System.Collections.Generic;

namespace OHM.Nodes.Commands
{
    /// <summary>
    /// Core command interface for all node in the system
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Unique key of the command for the corresponding node
        /// </summary>
        string Key { get; }

        /// <summary>
        /// Name of the command for the corresponding node
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Short description of the command for the corresponding nonde
        /// </summary>
        ICommandDefinition Definition { get; }

        /// <summary>
        /// Execute method of the command
        /// </summary>
        /// <param name="arguments">List of arguments passed to the command as string dictionnary</param>
        /// <returns>True when the command is executed</returns>
        bool Execute(IDictionary<string, string> arguments);

        /// <summary>
        /// Allow to check if the command can be executed with the internal state of the node or the command
        /// </summary>
        /// <returns>True when the command can be executed otherwise false</returns>
        bool CanExecute();

        //TEMPORARY SHIT. MUST BE MOVED IN PRIVATE AREA IN ABSTRACT COMMAND WHEN APP WILL BE FULLY DECOUPLED FROM INTERNAL BEHAVIOR
        bool ValidateArguments(IDictionary<string, string> arguments);
    }
}
