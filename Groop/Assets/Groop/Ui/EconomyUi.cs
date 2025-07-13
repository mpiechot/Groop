#nullable enable

using Groop.Exceptions;
using TMPro;
using UnityEngine;

namespace Groop.Ui
{
    /// <summary>
    /// Represents the UI for displaying the player's economy, specifically the amount of money they have.
    /// </summary>
    public class EconomyUi : MonoBehaviour
    {
        [SerializeField]
        private TMP_Text? moneyText;

        private TMP_Text MoneyText => SerializeFieldNotAssignedException.ThrowIfNull(moneyText);

        /// <summary>
        /// Sets the amount of money displayed in the UI.
        /// </summary>
        /// <param name="money">The amount of money to display.</param>
        public void SetMoney(int money)
        {
            UpdateMoneyText(money);
        }

        private void UpdateMoneyText(int money)
        {
            MoneyText.text = $"{money}";
        }
    }
}