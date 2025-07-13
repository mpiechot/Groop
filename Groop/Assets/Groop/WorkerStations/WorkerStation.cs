#nullable enable

using Groop.Exceptions;
using Groop.Plants;
using Groop.Workers;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Groop.WorkerStations
{
    /// <summary>
    /// Represents a worker station in the game that manages available workers and their assignments to plants.
    /// </summary>
    public class WorkerStation : MonoBehaviour
    {
        [SerializeField]
        private List<Worker> availableWorkers = new();

        private GameContext? gameContext;

        private GameContext GameContext => NotInitializedException.ThrowIfNull(gameContext);

        /// <summary>
        /// Initializes the <see cref="WorkerStation"/>.
        /// </summary>
        /// <param name="gameContext">The context to use for the worker station.</param>
        public void Initialize(GameContext gameContext)
        {
            this.gameContext = gameContext;
            foreach (var worker in availableWorkers)
            {
                worker.gameObject.SetActive(false);
            }
        }

        /// <summary>
        /// Attempts to send a worker of the specified type to the target plant.
        /// </summary>
        /// <param name="typeToSend">The type of worker to send.</param>
        /// <param name="targetPlant">The plant to which the worker should be sent.</param>
        /// <returns>True if a worker was successfully sent, false otherwise.</returns>
        public bool TrySendWorkerToPlant(WorkerType typeToSend, Plant targetPlant)
        {
            var workerToSend = availableWorkers.FirstOrDefault(worker => worker.WorkerType == typeToSend);

            if (workerToSend == null)
            {
                // No available worker of the specified type
                return false;
            }

            availableWorkers.Remove(workerToSend);
            workerToSend.gameObject.SetActive(true);
            workerToSend.SendToPlant(targetPlant);
            return true;
        }

        /// <summary>
        /// Returns a worker to the station after it has completed its task.
        /// </summary>
        /// <param name="worker">The worker to return.</param>
        public void ReturnWorker(Worker worker)
        {
            if (worker == null)
            {
                return;
            }

            worker.gameObject.SetActive(false);
            availableWorkers.Add(worker);

            var plant = worker.ResetTargetPlant();

            switch (worker.WorkerType)
            {
                case WorkerType.Gardener:
                    if (plant == null)
                    {
                        Debug.LogWarning("Worker returned without a plant as target.");
                        return;
                    }
                    GameContext.PlantsLifecycleObserver.AddPlant(plant);
                    break;
                case WorkerType.Harvester:
                    GameContext.GameController.MakeProfit(worker.ProfitForCurrentPlant);
                    break;
                default:
                    Debug.LogWarning($"Worker type {worker.WorkerType} is not handled in ReturnWorker method.");
                    break;
            }
        }
    }
}
