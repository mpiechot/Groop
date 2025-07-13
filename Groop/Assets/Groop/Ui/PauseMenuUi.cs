#nullable enable

using Groop.Exceptions;
using System;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Groop.Ui
{
    /// <summary>
    /// Represents the UI for the pause menu in the game.
    /// </summary>
    public class PauseMenuUi : MonoBehaviour
    {
        [SerializeField]
        private InputAction? openPauseMenuInput;

        private GameContext? gameContext;

        private GameContext GameContext => NotInitializedException.ThrowIfNull(gameContext);

        private InputAction OpenPauseMenuInput => SerializeFieldNotAssignedException.ThrowIfNull(openPauseMenuInput);

        /// <summary>
        /// Initializes the <see cref="PauseMenuUi"/>.
        /// </summary>
        /// <param name="context">The context to use for the UI.</param>
        public void Initialize(GameContext context)
        {
            gameContext = context;

            // Ensure the pause menu is hidden at the start
            HidePauseMenu();

            OpenPauseMenuInput.Enable();
            OpenPauseMenuInput.performed += OnOpenPauseMenuInputPerformed;
        }

        private void OnOpenPauseMenuInputPerformed(InputAction.CallbackContext context)
        {
            gameObject.SetActive(true);
            GameContext.GameController.PauseGame();
        }

        /// <summary>
        /// Shows the pause menu and pauses the game.
        /// </summary>
        public void ShowPauseMenu()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Hides the pause menu and resumes the game.
        /// </summary>
        public void HidePauseMenu()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Handles the resume button click event to hide the pause menu and resume the game.
        /// </summary>
        public void OnResumeButtonClicked()
        {
            HidePauseMenu();
            GameContext.GameController.ResumeGame();
        }

        /// <summary>
        /// Handles the quit button click event to hide the pause menu and quit the game.
        /// </summary>
        public void OnQuitButtonClicked()
        {
            // Ensure that the pause menu is hidden before quitting and the game is not paused anymore
            OnResumeButtonClicked();
            GameContext.GameController.QuitGame();
        }

        private void OnDestroy()
        {
            OpenPauseMenuInput.performed -= OnOpenPauseMenuInputPerformed;
        }
    }
}