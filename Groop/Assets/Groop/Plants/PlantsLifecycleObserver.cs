#nullable enable

using Groop.Exceptions;
using Groop.Workers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Groop.Plants
{
    /// <summary>
    /// Observes the lifecycle of plants in the game. Informs the <see cref="WorkerStations.WorkerStation"/> when plants need any action, such as watering or harvesting.
    /// </summary>
    public class PlantsLifecycleObserver : MonoBehaviour
    {
        private List<Plant> growingPlants = new();

        private GameContext? gameContext;

        private GameContext GameContext => NotInitializedException.ThrowIfNull(gameContext);

        private void Update()
        {
            if (growingPlants.Count == 0)
            {
                return;
            }

            var plantsToWater = growingPlants.Where(growingPlant => growingPlant.PlantState == PlantState.NeedsWater && !growingPlant.IsWorkerMovingToPlant).ToArray();
            foreach (var plant in plantsToWater)
            {
                if (GameContext.WorkerStation.TrySendWorkerToPlant(WorkerType.Gardener, plant))
                {
                    plant.IsWorkerMovingToPlant = true;
                }
            }

            var plantsToHarvest = growingPlants.Where(growingPlant => growingPlant.PlantState == PlantState.FullyGrown && !growingPlant.IsWorkerMovingToPlant).ToArray();
            foreach (var plant in plantsToHarvest)
            {
                if (GameContext.WorkerStation.TrySendWorkerToPlant(WorkerType.Harvester, plant))
                {
                    plant.IsWorkerMovingToPlant = true;
                }
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="PlantsLifecycleObserver"/>.
        /// </summary>
        /// <param name="gameContext">The context to use for the observer.</param>
        public void Initialize(GameContext gameContext)
        {
            this.gameContext = gameContext;
        }

        /// <summary>
        /// Adds a plant to the observer's list of growing plants. 
        /// If the plant is already in the list, it will not be added again.
        /// </summary>
        /// <param name="plant">The plant to add.</param>
        public void AddPlant(Plant plant)
        {
            if (growingPlants.Contains(plant))
            {
                return;
            }

            growingPlants.Add(plant);
        }
    }
}
