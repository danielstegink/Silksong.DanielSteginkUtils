using UnityEngine;

namespace DanielSteginkUtils.Utilities
{
    /// <summary>
    /// Library for interacting with Unity components
    /// </summary>
    public static class Components
    {
        /// <summary>
        /// Removes the given component from the object if it exists
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="gameObject"></param>
        public static void RemoveComponent<T>(this GameObject gameObject) where T : Component
        {
            T component = gameObject.GetComponent<T>();
            if (component != null)
            {
                UnityEngine.Object.DestroyImmediate(component);
            }
        }
    }
}