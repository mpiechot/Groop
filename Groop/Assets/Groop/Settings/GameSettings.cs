using Groop.Plants;
using UnityEngine;

namespace Groop.Settings
{
    /// <summary>
    /// Represents the game settings that can be configured in the Unity Editor.
    /// </summary>
    [CreateAssetMenu(fileName = "GameSettings", menuName = "Groop/GameSettings")]
    public class GameSettings : ScriptableObject
    {
        /// <summary>
        ///   Gets or sets the initial amount of money the player starts with in the game.
        /// </summary>
        [field: SerializeField]
        public int InitialMoney { get; private set; }

        /// <summary>
        /// Gets or sets the plant blueprint for tomatos.
        /// </summary>
        [field: SerializeField]
        public PlacablePlant TomatoPlacablePlant { get; private set; }

        /// <summary>
        /// Gets or sets the plant blueprint for corn.
        /// </summary>
        [field: SerializeField]
        public PlacablePlant CornPlacablePlant { get; private set; }
    }
}
