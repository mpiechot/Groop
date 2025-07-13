#nullable enable

using Groop.Plants;
using Groop.Settings;
using Groop.WorkerStations;

namespace Groop
{
    /// <summary>
    /// Represents the context of the game, containing references to various components which need to be accessed by other components.
    /// </summary>
    public class GameContext
    {
        /// <summary>
        /// Gets the UI controller for the game, responsible for managing the game's user interface.
        /// </summary>
        public GameUiController GameUiController { get; }

        /// <summary>
        /// Gets the controller for managing the game logic and state.
        /// </summary>
        public GameController GameController { get; }

        /// <summary>
        /// Gets the settings for the game, including configurations for plants, worker stations, and other gameplay elements.
        /// </summary>
        public GameSettings GameSettings { get; }

        /// <summary>
        /// Gets the worker station that manages available workers and their assignments to plants.
        /// </summary>
        public WorkerStation WorkerStation { get; }

        /// <summary>
        /// Gets the observer for plant lifecycle events, which tracks the growth and lifecycle of plants in the game.
        /// </summary>
        public PlantsLifecycleObserver PlantsLifecycleObserver { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GameContext"/> class with the specified components.
        /// </summary>
        /// <param name="workerStation">The worker station that manages workers.</param>
        /// <param name="gameUiController">The UI controller for the game.</param>
        /// <param name="gameSettings">The settings for the game.</param>
        /// <param name="plantsLifecycleObserver">The observer for plant lifecycle events.</param>
        public GameContext(WorkerStation workerStation, GameUiController gameUiController, GameSettings gameSettings, PlantsLifecycleObserver plantsLifecycleObserver)
        {
            this.GameUiController = gameUiController;
            this.GameSettings = gameSettings;
            this.WorkerStation = workerStation;
            this.PlantsLifecycleObserver = plantsLifecycleObserver;
            this.GameController = new(this);
        }
    }
}
