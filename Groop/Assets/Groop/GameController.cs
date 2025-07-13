#nullable enable

using Groop.Exceptions;
using Groop.Plants;
using System;
using UnityEngine;

namespace Groop
{
    /// <summary>
    /// Controls the game state, including starting, pausing, and quitting the game.
    /// </summary>
    public class GameController
    {
        private int currentMoney;

        private GameContext? gameContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameController"/> class with the specified game context.
        /// </summary>
        /// <param name="context">The context to use for the game controller.</param>
        public GameController(GameContext context)
        {
            gameContext = context;
        }

        /// <summary>
        /// Gets the current amount of money available to the player.
        /// </summary>
        public int CurrentMoney => currentMoney;

        private GameContext GameContext => NotInitializedException.ThrowIfNull(gameContext);

        /// <summary>
        /// Pauses the game.
        /// </summary>
        public void PauseGame()
        {
            Time.timeScale = 0f; // Pause the game
        }

        /// <summary>
        /// Resumes the game.
        /// </summary>
        public void ResumeGame()
        {
            Time.timeScale = 1f; // Resume the game
        }

        /// <summary>
        /// Quits the game.
        /// </summary>
        public void QuitGame()
        {
            Application.Quit(); // Quit the application
        }

        /// <summary>
        /// Starts the game.
        /// </summary>
        public void StartGame()
        {
            currentMoney = GameContext.GameSettings.InitialMoney;
        }

        /// <summary>
        /// Plants tomato seeds in the specified plant and updates the money accordingly.
        /// </summary>
        /// <param name="tomatoPlant">The plant in which to plant the seeds.</param>
        /// <exception cref="InvalidOperationException">Thrown if the tomato plant is null.</exception>
        public void TomatoSeedsPlanted(Plant? tomatoPlant)
        {
            if (tomatoPlant == null)
            {
                throw new InvalidOperationException("Current plant cannot be null when planting seeds.");
            }

            currentMoney -= GameContext.GameSettings.TomatoPlacablePlant.Cost;
            GameContext.PlantsLifecycleObserver.AddPlant(tomatoPlant);
            GameContext.GameUiController.UpdateMoneyDisplay(currentMoney);
        }

        /// <summary>
        /// Plants corn seeds in the specified plant and updates the money accordingly.
        /// </summary>
        /// <param name="cornPlant">The plant in which to plant the seeds.</param>
        /// <exception cref="InvalidOperationException">Thrown if the corn plant is null.</exception>
        public void CornSeedsPlanted(Plant? cornPlant)
        {
            if (cornPlant == null)
            {
                throw new InvalidOperationException("Current plant cannot be null when planting seeds.");
            }

            currentMoney -= GameContext.GameSettings.CornPlacablePlant.Cost;
            GameContext.PlantsLifecycleObserver.AddPlant(cornPlant);
            GameContext.GameUiController.UpdateMoneyDisplay(currentMoney);
        }

        /// <summary>
        /// Updates the current money by adding the profit made from selling the fully grown plant.
        /// </summary>
        /// <param name="plantProfit">The profit made from the plant.</param>
        public void MakeProfit(int plantProfit)
        {
            currentMoney += plantProfit;
            GameContext.GameUiController.UpdateMoneyDisplay(currentMoney);
        }
    }
}
