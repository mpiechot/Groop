#nullable enable

using Groop.Exceptions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Groop.Fields
{
    /// <summary>
    /// Handles click events on fields in the game.
    /// </summary>
    public class FieldClickHandler : MonoBehaviour
    {
        [SerializeField]
        private InputAction? clickAction;

        [SerializeField]
        private InputAction? positionAction;

        private InputAction ClickAction => SerializeFieldNotAssignedException.ThrowIfNull(clickAction);

        private InputAction PositionAction => SerializeFieldNotAssignedException.ThrowIfNull(positionAction);

        private Camera? mainCamera;

        private GameContext? gameContext;

        private GameContext GameContext => NotInitializedException.ThrowIfNull(gameContext);

        private Camera MainCamera => NotInitializedException.ThrowIfNull(mainCamera);

        /// <summary>
        /// Initializes the <see cref="FieldClickHandler"/>.
        /// </summary>
        /// <param name="context">The context to use</param>
        public void Initialize(GameContext context)
        {
            gameContext = context;
            mainCamera = Camera.main!;

            // Ensure the click handler is ready to handle clicks
            ClickAction.Enable();
            PositionAction.Enable();
            ClickAction.performed += OnClickPerformed;
        }

        private void OnClickPerformed(InputAction.CallbackContext ctx)
        {
            var screenPos = PositionAction.ReadValue<Vector2>();
            var ray = MainCamera.ScreenPointToRay(screenPos);

            if (Physics.Raycast(ray, out var hit, 1000, LayerMask.GetMask("UI")))
            {
                return;
            }
            else if (Physics.Raycast(ray, out hit, 1000, LayerMask.GetMask("Field")))
            {
                var field = hit.transform.GetComponent<Field>();
                GameContext.GameUiController.ShowFieldSeedSelectorAt(field);
            }
            else
            {
                GameContext.GameUiController.HideFieldSeedSelector();
            }
        }

        private void OnDestroy()
        {
            ClickAction.performed -= OnClickPerformed;
        }
    }
}
