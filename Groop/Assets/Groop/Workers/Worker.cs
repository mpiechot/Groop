#nullable enable

using Groop.Exceptions;
using Groop.Plants;
using Groop.WorkerStations;
using UnityEngine;
using UnityEngine.AI;

namespace Groop.Workers
{
    /// <summary>
    /// Represents a worker in the game that can perform actions on plants, such as watering or harvesting.
    /// </summary>
    [RequireComponent(typeof(Animator), typeof(NavMeshAgent))]
    public class Worker : MonoBehaviour
    {
        [SerializeField]
        private WorkerConfiguration? workerConfiguration;

        [SerializeField]
        private WorkerStation? workerStation;

        private Animator workerAnimator;

        private NavMeshAgent workerAgent;

        private Plant? targetPlant;

        /// <summary>
        /// Gets the profit for the current plant that the worker is assigned to.
        /// </summary>
        public int ProfitForCurrentPlant { get; private set; }

        private bool isMovingToStation;

        private WorkerConfiguration WorkerConfiguration => SerializeFieldNotAssignedException.ThrowIfNull(workerConfiguration);

        private WorkerStation WorkerStation => SerializeFieldNotAssignedException.ThrowIfNull(workerStation);

        /// <summary>
        /// Gets the type of the worker, which determines its role in the game (e.g., Gardener, Harvester).
        /// </summary>
        public WorkerType WorkerType => WorkerConfiguration.Type;

        private void Awake()
        {
            workerAnimator = GetComponent<Animator>();
            workerAgent = GetComponent<NavMeshAgent>();

            workerAgent.speed = WorkerConfiguration.MoveSpeed;
        }

        private void Update()
        {
            if (!workerAgent.pathPending && workerAgent.remainingDistance < 1.2f)
            {
                if (isMovingToStation)
                {
                    WorkerStation.ReturnWorker(this);
                }
                else
                {
                    if (workerAgent.isStopped)
                    {
                        // If the agent is stopped, it means the action is already being performed
                        return;
                    }

                    workerAgent.isStopped = true;
                    workerAnimator.SetTrigger("PerformAction");
                }
            }
        }

        /// <summary>
        /// Resets the target plant for this worker.
        /// </summary>
        /// <returns>The target plant before resetting, or null if no target plant was set.</returns>
        public Plant? ResetTargetPlant()
        {
            var plantToReturn = targetPlant;
            targetPlant = null;
            return plantToReturn;
        }

        /// <summary>
        /// Sends the worker to a specified plant to perform an action (like watering or harvesting).
        /// </summary>
        /// <param name="plantToSend">The plant to which the worker will be sent.</param>
        public void SendToPlant(Plant plantToSend)
        {
            targetPlant = plantToSend;
            ProfitForCurrentPlant = targetPlant.PlantProfit;
            workerAgent.SetDestination(plantToSend.transform.position);
            workerAnimator.SetTrigger("Walk");
            isMovingToStation = false;
        }

        /// <summary>
        /// Called when the worker has finished performing an action on a plant (like watering or harvesting).
        /// </summary>
        public void OnPerformActionFinished()
        {
            isMovingToStation = true;
            workerAgent.SetDestination(WorkerStation.transform.position);
            workerAgent.isStopped = false;

            if (targetPlant == null)
            {
                return;
            }

            switch (WorkerConfiguration.Type)
            {
                case WorkerType.Gardener:
                    targetPlant.Water();
                    break;
                case WorkerType.Harvester:
                    targetPlant.Harvest();
                    break;
            }
        }
    }
}
