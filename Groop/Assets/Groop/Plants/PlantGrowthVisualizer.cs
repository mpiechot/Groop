#nullable enable

using Groop.Exceptions;
using UnityEngine;
using UnityEngine.UI;

namespace Groop.Plants
{
    /// <summary>
    /// Visualizes the growth state of a plant.
    /// </summary>
    public class PlantGrowthVisualizer : MonoBehaviour
    {
        [SerializeField]
        private Image? growTimerImage;

        [SerializeField]
        private Image? needWaterImage;

        [SerializeField]
        private Image? canHarvestImage;

        private Image GrowTimerImage => SerializeFieldNotAssignedException.ThrowIfNull(growTimerImage);

        private Image NeedWaterImage => SerializeFieldNotAssignedException.ThrowIfNull(needWaterImage);

        private Image CanHarvestImage => SerializeFieldNotAssignedException.ThrowIfNull(canHarvestImage);

        private void Awake()
        {
            HideGrowthTimer();
            HideNeedWater();
            HideCanHarvest();
        }

        /// <summary>
        /// Shows the growth timer for the plant.
        /// </summary>
        /// <param name="timerValue">The value to set the fill amount of the growth timer image. In the range of 0 to 1. Where 1 means no growth and 0 means fully grown.</param>
        public void ShowGrowthTimer(float timerValue)
        {
            GrowTimerImage.fillAmount = timerValue;
            GrowTimerImage.gameObject.SetActive(true);
        }

        /// <summary>
        /// Hides the growth timer for the plant.
        /// </summary>
        public void HideGrowthTimer()
        {
            GrowTimerImage.gameObject.SetActive(false);
        }

        /// <summary>
        /// Shows the need for water indicator for the plant.
        /// </summary>
        public void ShowNeedWater()
        {
            NeedWaterImage.gameObject.SetActive(true);
        }

        /// <summary>
        /// Hides the need for water indicator for the plant.
        /// </summary>
        public void HideNeedWater()
        {
            NeedWaterImage.gameObject.SetActive(false);
        }

        /// <summary>
        /// Shows the can harvest indicator for the plant.
        /// </summary>
        public void ShowCanHarvest()
        {
            CanHarvestImage.gameObject.SetActive(true);
        }

        /// <summary>
        /// Hides the can harvest indicator for the plant.
        /// </summary>
        public void HideCanHarvest()
        {
            CanHarvestImage.gameObject.SetActive(false);
        }
    }
}
