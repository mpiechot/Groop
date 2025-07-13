namespace Groop.Plants
{
    /// <summary>
    /// Represents the different states a plant can be in during its lifecycle.
    /// </summary>
    public enum PlantState
    {
        /// <summary>
        /// The plant is in the seed growing stage, where it is being planted and starting to grow.
        /// </summary>
        SeedGrow,

        /// <summary>
        /// The plant needs water to continue growing.
        /// </summary>
        NeedsWater,

        /// <summary>
        /// The plant is currently growing to its full size.
        /// </summary>
        PlantGrow,

        /// <summary>
        /// The plant has fully grown and is ready to be harvested.
        /// </summary>
        FullyGrown,

        /// <summary>
        /// The plant has been harvested and is now transported to the worker station.
        /// </summary>
        Collected
    }
}
