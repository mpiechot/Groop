#nullable enable

using Groop.Exceptions;
using Groop.Fields;
using System;
using UnityEngine;
using UnityEngine.UI;

namespace Groop.Ui
{
    /// <summary>
    /// Represents the UI for selecting seeds to plant in a field.
    /// </summary>
    public class FieldSeedSelectorUi : MonoBehaviour
    {
        [SerializeField]
        private Button? tomatoButton;

        [SerializeField]
        private Button? cornButton;

        [SerializeField]
        private Vector3 PositionalOffset = new Vector3(0, 1, 0);

        private Button TomatoButton => SerializeFieldNotAssignedException.ThrowIfNull(tomatoButton);

        private Button CornButton => SerializeFieldNotAssignedException.ThrowIfNull(cornButton);

        private Field? currentField;

        private GameContext? gameContext;

        private GameContext GameContext => NotInitializedException.ThrowIfNull(gameContext);

        /// <summary>
        /// Initializes a new instance of the <see cref="FieldSeedSelectorUi"/> class.
        /// </summary>
        /// <param name="context">The context to use for the UI.</param>
        public void Initialize(GameContext context)
        {
            gameContext = context;
            Hide();
        }

        /// <summary>
        /// Shows the seed selector UI for a specific field.
        /// </summary>
        /// <param name="field">The field to show the seed selector for.</param>
        /// <param name="currentMoney">The current amount of money available to the player.</param>
        public void ShowForField(Field field, int currentMoney)
        {
            currentField = field;
            gameObject.transform.position = field.transform.position + PositionalOffset;
            UpdateSeedButtonInteractability(
                currentMoney,
                GameContext.GameSettings.TomatoPlacablePlant.Cost,
                GameContext.GameSettings.CornPlacablePlant.Cost);
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Plants a tomato in the currently selected field.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if no field was selected before.</exception>
        public void PlantTomato()
        {
            if (currentField == null)
            {
                throw new InvalidOperationException("No field selected for planting.");
            }

            currentField.PlacePlant(GameContext.GameSettings.TomatoPlacablePlant);
            GameContext.GameController.TomatoSeedsPlanted(currentField.CurrentPlant);

            Hide();
        }

        /// <summary>
        /// Plants corn in the currently selected field.
        /// </summary>
        /// <exception cref="InvalidOperationException">Thrown if no field was selected before.</exception>
        public void PlantCorn()
        {
            if (currentField == null)
            {
                throw new InvalidOperationException("No field selected for planting.");
            }

            currentField.PlacePlant(GameContext.GameSettings.CornPlacablePlant);
            GameContext.GameController.CornSeedsPlanted(currentField.CurrentPlant);

            Hide();
        }

        /// <summary>
        /// Hides the seed selector UI.
        /// </summary>
        public void Hide()
        {
            gameObject.SetActive(false);
        }

        private void UpdateSeedButtonInteractability(int currentMoney, int tomatoCost, int cornCost)
        {
            if (currentMoney < tomatoCost)
            {
                TomatoButton.interactable = false;
            }
            else
            {
                TomatoButton.interactable = true;
            }

            if (currentMoney < cornCost)
            {
                CornButton.interactable = false;
            }
            else
            {
                CornButton.interactable = true;
            }
        }
    }
}