#nullable enable

using Groop.Exceptions;
using Groop.Fields;
using Groop.Ui;
using UnityEngine;

namespace Groop
{
    /// <summary>
    /// Controls the game UI, including the field seed selector, pause menu, and economy display.
    /// </summary>
    public class GameUiController : MonoBehaviour
    {
        [SerializeField]
        private FieldSeedSelectorUi? fieldSeedSelectorUi;

        [SerializeField]
        private PauseMenuUi? pauseMenuUi;

        [SerializeField]
        private EconomyUi? economyUi;

        private GameContext? gameContext;

        private GameContext GameContext => NotInitializedException.ThrowIfNull(gameContext);

        private FieldSeedSelectorUi FieldSeedSelectorUi => SerializeFieldNotAssignedException.ThrowIfNull(fieldSeedSelectorUi);

        private PauseMenuUi PauseMenuUi => SerializeFieldNotAssignedException.ThrowIfNull(pauseMenuUi);

        private EconomyUi EconomyUi => SerializeFieldNotAssignedException.ThrowIfNull(economyUi);

        /// <summary>
        /// Initializes the <see cref="GameUiController"/> with the provided game context.
        /// </summary>
        /// <param name="context">The context to use for the UI.</param>
        public void Initialize(GameContext context)
        {
            gameContext = context;
            EconomyUi.SetMoney(context.GameController.CurrentMoney);
            FieldSeedSelectorUi.Initialize(context);
            PauseMenuUi.Initialize(context);
        }

        /// <summary>
        /// Resets the game UI to its initial state.
        /// </summary>
        public void ResetGameUi()
        {
            HideFieldSeedSelector();
            PauseMenuUi.HidePauseMenu();
        }

        /// <summary>
        /// Shows the pause menu and pauses the game.
        /// </summary>
        /// <param name="selectedField"></param>
        public void ShowFieldSeedSelectorAt(Field selectedField)
        {
            FieldSeedSelectorUi.ShowForField(selectedField, GameContext.GameController.CurrentMoney);
        }

        /// <summary>
        /// Hides the field seed selector UI.
        /// </summary>
        public void HideFieldSeedSelector()
        {
            FieldSeedSelectorUi.Hide();
        }

        /// <summary>
        /// Updates the money display.
        /// </summary>
        /// <param name="money">The amount of money to display.</param>
        public void UpdateMoneyDisplay(int money)
        {
            EconomyUi.SetMoney(money);
        }
    }
}
