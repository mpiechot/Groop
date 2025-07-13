#nullable enable

namespace Groop.Workers
{
    /// <summary>
    /// Enumeration representing the type of worker in the game.
    /// </summary>
    public enum WorkerType
    {
        /// <summary>
        /// Represents a worker that performs gardening tasks, such as watering plants.
        /// </summary>
        Gardener,

        /// <summary>
        /// Represents a worker that performs harvesting tasks, such as collecting fully grown plants.
        /// </summary>
        Harvester,
    }
}
