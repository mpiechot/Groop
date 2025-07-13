#nullable enable

using Cysharp.Threading.Tasks;
using Groop.Exceptions;
using Groop.Fields;
using Groop.Util;
using System;
using System.Threading;
using UnityEngine;

namespace Groop.Plants
{
    /// <summary>
    /// Represents a plant in the game that can grow, be watered, and harvested.
    /// </summary>
    public class Plant : MonoBehaviour, IDisposable
    {
        [SerializeField]
        private PlantGrowthVisualizer? grothVisualizer;

        private CancellableTaskCollection taskCollection = new();

        private PlacablePlant? plantBlueprint;

        private GameObject? createdPlant;

        private Field? field;

        private Field Field => NotInitializedException.ThrowIfNull(field);

        private PlacablePlant PlantBlueprint => NotInitializedException.ThrowIfNull(plantBlueprint);

        private PlantGrowthVisualizer GrowthVisualizer => SerializeFieldNotAssignedException.ThrowIfNull(grothVisualizer);

        /// <summary>
        /// Gets the current growth state of the plant.
        /// </summary>
        public PlantState PlantState { get; private set; } = PlantState.SeedGrow;

        /// <summary>
        /// Gets the value of this plants profit when harvested.
        /// </summary>
        public int PlantProfit => PlantBlueprint.Profit;

        /// <summary>
        /// Gets or sets a value indicating whether a worker is currently moving to this plant to perform an action (like watering or harvesting).
        /// </summary>
        public bool IsWorkerMovingToPlant { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Plant"/>.
        /// </summary>
        /// <param name="plantBlueprint">The blueprint of the plant to be initialized.</param>
        public void Initialize(PlacablePlant plantBlueprint, Field field)
        {
            this.plantBlueprint = plantBlueprint;
            this.field = field;
            createdPlant = Instantiate(plantBlueprint.SeedPrefab, transform.position, Quaternion.identity, transform);
            StartGrowing();
        }

        public void OnDestroy()
        {
            Dispose();
        }

        /// <summary>
        /// Disposes of the plant, cleaning up resources and stopping any ongoing tasks.
        /// </summary>
        public void Dispose()
        {
            taskCollection.Dispose();
            if (createdPlant != null)
            {
                Destroy(createdPlant);
                createdPlant = null;
            }
        }

        /// <summary>
        /// Water the plant if it is in the state that requires watering.
        /// </summary>
        public void Water()
        {
            if (PlantState != PlantState.NeedsWater)
            {
                return;
            }

            PlantState = PlantState.PlantGrow;
            IsWorkerMovingToPlant = false;
        }

        /// <summary>
        /// Harvest the plant if it is fully grown.
        /// </summary>
        public void Harvest()
        {
            if (PlantState != PlantState.FullyGrown)
            {
                return;
            }

            PlantState = PlantState.Collected;
            GrowthVisualizer.HideCanHarvest();
            IsWorkerMovingToPlant = false;
            Field.CollectPlant();
        }

        private void StartGrowing()
        {
            taskCollection.CancelExecution();
            taskCollection.StartExecution(GrowAsync);
        }

        private async UniTask GrowAsync(CancellationToken cancellationToken)
        {
            var currentGrowDuration = PlantBlueprint.SeedGrowTime * 1f;

            while (currentGrowDuration > 0 && !cancellationToken.IsCancellationRequested)
            {
                await UniTask.Yield(cancellationToken);
                currentGrowDuration -= Time.deltaTime * 1000;

                GrowthVisualizer.ShowGrowthTimer(Mathf.Clamp01(currentGrowDuration / PlantBlueprint.SeedGrowTime));
            }

            GrowthVisualizer.HideGrowthTimer();
            PlantState = PlantState.NeedsWater;
            GrowthVisualizer.ShowNeedWater();

            Destroy(createdPlant);
            createdPlant = Instantiate(PlantBlueprint.PlantPrefab, transform.position, Quaternion.identity, transform);

            // Wait until the plant is watered
            while (PlantState != PlantState.PlantGrow && !cancellationToken.IsCancellationRequested)
            {
                await UniTask.Yield(cancellationToken);
            }
            GrowthVisualizer.HideNeedWater();
            // Start the last growth phase
            currentGrowDuration = PlantBlueprint.PlantGrowTime * 1f;

            while (currentGrowDuration > 0 && !cancellationToken.IsCancellationRequested)
            {
                await UniTask.Yield(cancellationToken);
                currentGrowDuration -= Time.deltaTime * 1000;

                GrowthVisualizer.ShowGrowthTimer(Mathf.Clamp01(currentGrowDuration / PlantBlueprint.PlantGrowTime));
            }

            GrowthVisualizer.HideGrowthTimer();
            PlantState = PlantState.FullyGrown;
            GrowthVisualizer.ShowCanHarvest();
        }
    }
}
