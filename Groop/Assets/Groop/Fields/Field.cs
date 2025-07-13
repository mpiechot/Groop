#nullable enable

using Groop.Exceptions;
using Groop.Plants;
using UnityEngine;

namespace Groop.Fields
{
    /// <summary>
    /// Represents a field in the game where plants can be placed.
    /// </summary>
    public class Field : MonoBehaviour
    {
        private Plant? plant;

        [SerializeField]
        private Plant? plantPrefab;

        private Plant PlantPrefab => SerializeFieldNotAssignedException.ThrowIfNull(plantPrefab);

        /// <summary>
        /// Gets the currently placed plant on this field or null if no plant is placed.
        /// </summary>
        public Plant? CurrentPlant => plant;

        /// <summary>
        /// Places a plant in this field.
        /// </summary>
        /// <param name="placablePlant">The plant to be placed in the field.</param>
        public void PlacePlant(PlacablePlant placablePlant)
        {
            if (plant != null)
            {
                Debug.LogWarning("A plant is already placed in this field.");
                return;
            }

            plant = Instantiate(PlantPrefab, transform.position, Quaternion.identity, transform);
            plant.Initialize(placablePlant, this);
        }

        public void CollectPlant()
        {
            if (plant == null)
            {
                Debug.LogWarning("No plant to collect in this field.");
                return;
            }

            Destroy(plant.gameObject);
            plant = null;
        }
    }
}
