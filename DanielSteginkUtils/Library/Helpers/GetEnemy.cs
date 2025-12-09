using GlobalEnums;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DanielSteginkUtils.Helpers
{
    /// <summary>
    /// Library for getting enemies
    /// </summary>
    public static class GetEnemy
    {
        /// <summary>
        /// Gets a list of all enemies within a provided range
        /// </summary>
        /// <param name="maxDistance"></param>
        /// <returns></returns>
        public static List<GameObject> GetEnemies(float maxDistance)
        {
            // Get all active game objects
            GameObject[] gameObjects = UnityEngine.Object.FindObjectsByType<GameObject>(FindObjectsSortMode.None)
                                                            .Where(x => x.activeSelf).ToArray();
            if (gameObjects.Length == 0)
            {
                return new List<GameObject>();
            }

            // Filter out game objects that aren't enemies
            GameObject[] enemies = gameObjects.Where(x => IsEnemy(x))
                                                .ToArray();
            if (enemies.Length == 0)
            {
                return new List<GameObject>();
            }

            // Determines which enemies are in the provided range
            Transform playerPosition = HeroController.instance.gameObject.transform;
            List<GameObject> enemiesInRange = enemies.Where(x => GetDistance(x.transform, playerPosition) <= maxDistance)
                                                        .ToList();
            return enemiesInRange;
        }

        /// <summary>
        /// Determines if an object is an enemy
        /// </summary>
        /// <param name="gameObject"></param>
        /// <returns></returns>
        public static bool IsEnemy(GameObject gameObject)
        {
            bool isEnemy = false;
            if (gameObject.layer == (int)PhysLayers.ENEMIES ||
                gameObject.tag == "Boss")
            {
                // Enemy must have a health bar for us to consider them a damageable enemy
                HealthManager health = gameObject.GetComponent<HealthManager>();
                if (health != default)
                {
                    isEnemy = true;
                }
            }

            return isEnemy;
        }

        /// <summary>
        /// Gets the distance between 2 objects
        /// </summary>
        /// <param name="enemy"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public static float GetDistance(Transform enemy, Transform player)
        {
            float xDiff = Math.Abs(enemy.GetPositionX() - player.GetPositionX());
            float yDiff = Math.Abs(enemy.GetPositionY() - player.GetPositionY());
            return (float)Math.Sqrt(xDiff * xDiff + yDiff * yDiff);
        }
    }
}