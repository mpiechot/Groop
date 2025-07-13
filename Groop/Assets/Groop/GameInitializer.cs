#nullable enable

using Groop.Exceptions;
using Groop.Fields;
using Groop.Plants;
using Groop.Settings;
using Groop.WorkerStations;
using UnityEngine;

namespace Groop
{
    /// <summary>
    /// Initializes the game by setting up the game context and its components.
    /// </summary>
    /// <remarks>
    /// This component represents the starting point of the game, where all necessary components are initialized and linked together.
    /// </remarks>
    public class GameInitializer : MonoBehaviour
    {
        [SerializeField]
        private GameUiController? gameUiController;

        [SerializeField]
        private GameSettings? gameSettings;

        [SerializeField]
        private FieldClickHandler? fieldClickHandler;

        [SerializeField]
        private WorkerStation? workerStation;

        [SerializeField]
        private PlantsLifecycleObserver? plantsLifecycleObserver;

        private void Start()
        {
            SerializeFieldNotAssignedException.ThrowIfNull(gameUiController);
            SerializeFieldNotAssignedException.ThrowIfNull(gameSettings);
            SerializeFieldNotAssignedException.ThrowIfNull(fieldClickHandler);
            SerializeFieldNotAssignedException.ThrowIfNull(workerStation);
            SerializeFieldNotAssignedException.ThrowIfNull(plantsLifecycleObserver);

            var gameContext = new GameContext(workerStation, gameUiController, gameSettings, plantsLifecycleObserver);

            gameContext.GameController.StartGame();

            gameUiController.Initialize(gameContext);
            fieldClickHandler.Initialize(gameContext);
            plantsLifecycleObserver.Initialize(gameContext);
            workerStation.Initialize(gameContext);
        }
    }
}
