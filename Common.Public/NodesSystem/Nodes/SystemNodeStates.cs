
namespace OHM.Nodes
{
    /// <summary>
    /// System Node States Enum
    /// </summary>
    public enum SystemNodeStates
    {
        /// <summary>
        /// It's going really bad. Node should not be trusted
        /// </summary>
        fatal,
        /// <summary>
        /// An error append but the node is partially working
        /// </summary>
        error,
        /// <summary>
        /// Some strange thing appening but should not alter the node
        /// </summary>
        warn,
        /// <summary>
        /// Node is not ready for operation. Node begin the creation stage
        /// </summary>
        creating,
        /// <summary>
        /// Node is not ready for operation. Node was created but still not initialized
        /// </summary>
        created,
        /// <summary>
        /// Node is not ready for operation. Node begin to initialization stage
        /// </summary>
        initializing,
        /// <summary>
        /// Node is now ready for operation. Fully initialized
        /// </summary>
        initialized,
        /// <summary>
        /// Node is operational in a normal way
        /// </summary>
        operational
    }
}
