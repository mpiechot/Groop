using UnityEngine;

namespace Groop.Workers
{
    /// <summary>
    /// Represents the configuration for a worker in the game, including its type and movement speed.
    /// </summary>
    [CreateAssetMenu(fileName = "WorkerConfiguration", menuName = "Groop/WorkerConfiguration")]
    public class WorkerConfiguration : ScriptableObject
    {
        /// <summary>
        ///     Gets or sets the worker type.
        /// </summary>
        [field: SerializeField]
        public WorkerType Type { get; private set; }

        /// <summary>
        ///     Gets or sets the workers move speed.
        /// </summary>
        [field: SerializeField]
        public float MoveSpeed { get; private set; }
    }
}
