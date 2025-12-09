namespace DanielSteginkUtils.Utilities
{
    /// <summary>
    /// Library for storing calculations related to Hollow Knight Notch Costs
    /// </summary>
    public static class NotchCosts
    {
        #region Nail / Needle
        /// <summary>
        /// The percent increase in nail damage per notch
        /// </summary>
        /// <returns></returns>
        public static float NailDamagePerNotch()
        {
            // Unbreakable Strength increases nail damage by 50%
            // It costs 3 notches, but per UnbreakableCharmCost its value should be 5 notches
            // That means a 10% increase in nail damage per notch.
            return 0.5f / UnbreakableCharmCost(3);
        }
        #endregion

        #region Spells / Tools
        /// <summary>
        /// The percent damage increase that all spells (and tools) should get per notch
        /// </summary>
        /// <returns></returns>
        public static float SpellDamagePerNotch()
        {
            // Shaman Stone increases each spell's damage by a different percent:
            // Vengeful Spirit gets 33%
            // Desolate Dive gets 51%
            // Howling Wraiths gets 50%

            // However, it also increases the size of Vengeful Spirit projectiles
            // If it didn't do this, it is somewhat reasonable to assume that
            // Vengeful Spirit's boost would also be about 50%

            // So for 1 notch, we should increase all spell damage by 50 / 3 = 16.67%
            return 0.5f / 3f;
        }

        /// <summary>
        /// The percent damage increase a single spell (or tool) should get per notch
        /// </summary>
        /// <returns></returns>
        public static float SingleSpellDamagePerNotch()
        {
            // Per above, Shaman Stone increases all spells by an average of 50% for 3 notches
            // It applies this increase to 3 groups of spells, so applying it to just one of them would be worth 1 notch
            // So for 1 notch we can increase the damage of a single spell by 50%
            return 3 * SpellDamagePerNotch();
        }

        /// <summary>
        /// The percent reduction in the cost of using a spell (or tool) per notch
        /// </summary>
        /// <returns></returns>
        public static float SpellDiscountPerNotch()
        {
            // Spell Twister uses 3 notches to reduce the cost of Spells from 33 to 24
            return (1 - 24 / 33) / 3;
        }
        #endregion

        #region Passive Regeneration
        /// <summary>
        /// The length of time (in seconds) it should take to regenerate 1 Mask for 1 notch
        /// </summary>
        /// <returns></returns>
        public static float PassiveHealTime()
        {
            // In HK, the Hiveblood charm regenerated 1 mask every 10 seconds for 4 notches
            float secondsPerMask = 10f;
            float secondsPerMaskPerNotch = secondsPerMask * 4;

            // So for 1 notch, we should take 40 seconds to regenerate a mask
            return secondsPerMaskPerNotch;
        }

        /// <summary>
        /// The length of time (in seconds) it should take to generate 1 point of Silk for 1 notch
        /// </summary>
        /// <returns></returns>
        public static float PassiveSilkTime()
        {
            // For 5 notches, Kingsoul gives 4 SOUL every 2 seconds, or 1 SOUL per 0.5 seconds
            float kingsoulSecondsPerSoul = 2f / 4f;

            // So for 1 notch, the player should get 0.4 SOUL per second, or 1 SOUL every 2.5 seconds
            float secondsPerSoulPerNotch = kingsoulSecondsPerSoul * 5;

            // 1 Silk is worth about 11 SOUL, so for 1 notch the player should get 1 Silk every 27.5 seconds
            return secondsPerSoulPerNotch / Calculations.GetSilkPerSoul();
        }
        #endregion

        /// <summary>
        /// The number of notches an Unbreakable charm would be worth without the cost of making it unbreakable
        /// </summary>
        /// <param name="baseCost"></param>
        /// <returns></returns>
        public static int UnbreakableCharmCost(int baseCost)
        {
            // I haven't found logic for this that satisfies me
            // Going with my gut, I think each fragile charm would be worth 2 extra notches if it didn't break when you died
            return baseCost + 2;
        }
    }
}