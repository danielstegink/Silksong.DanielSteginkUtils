using HarmonyLib;

namespace DanielSteginkUtils.Loggers
{
    /// <summary>
    /// Logs damage dealt to an enemy
    /// </summary>
    [HarmonyPatch(typeof(HealthManager), "TakeDamage")]
    public static class EnemyDamageLogger
    {
        /// <summary>
        /// Tracks whether or not to log damage effect
        /// </summary>
        private static bool isActive = false;

        [HarmonyPrefix]
        internal static void Prefix(HealthManager __instance, HitInstance hitInstance)
        {
            if (isActive &&
                hitInstance.Source != null)
            {
                Logging.Log("EnemyDamageLogger", $"Enemy {__instance.gameObject.name} taking {hitInstance.DamageDealt} damage (Type {hitInstance.AttackType}) from {hitInstance.Source.name}");
            }
        }

        /// <summary>
        /// Activates or deactivates the logger
        /// </summary>
        /// <param name="isActive">Status to set the logger's status to</param>
        public static void Toggle(bool isActive)
        {
            EnemyDamageLogger.isActive = isActive;
        }
    }
}