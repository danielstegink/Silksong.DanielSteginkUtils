namespace DanielSteginkUtils.Utilities
{
    /// <summary>
    /// Libray for numeric calculations
    /// </summary>
    public static class Calculations
    {
        /// <summary>
        /// Gets the Silk equivalent of 1 SOUL
        /// </summary>
        /// <returns></returns>
        public static float GetSilkPerSoul()
        {
            // In HK, the default max for SOUL is 99, while the default max for Silk in Silksong is 9
            // Therefore, 1 Silk is roughly equal to 11 SOUL
            float baseSoul = 99f;
            float baseSilk = 9f;
            return baseSilk / baseSoul;
        }
    }
}