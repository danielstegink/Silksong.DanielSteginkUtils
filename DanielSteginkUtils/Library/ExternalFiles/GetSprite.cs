using DanielSteginkUtils.Loggers;
using System.IO;
using System.Reflection;
using UnityEngine;

namespace DanielSteginkUtils.ExternalFiles
{
    /// <summary>
    /// Gets sprites from embedded resources in external assemblies
    /// </summary>
    public static class GetSprite
    {
        /// <summary>
        /// Gets sprite from embedded resources
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="assemblyName"></param>
        /// <param name="performLogging"></param>
        /// <returns></returns>
        public static Sprite? GetLocalSprite(string filePath, string assemblyName, bool performLogging = false)
        {
            //SharedData.Log($"Getting local sprite {spriteId}");
            string logPrefix = "GetSprite";
            Logging.Log(logPrefix, $"Getting sprite {filePath} in assembly '{assemblyName}'", performLogging);

            Assembly assembly = Assembly.Load(assemblyName);
            if (assembly == null)
            {
                Logging.Log(logPrefix, $"Assembly '{assemblyName}' not found");
                return null;
            }

            return GetLocalSprite(filePath, assembly, performLogging);
        }

        /// <summary>
        /// Gets sprite from embedded resources
        /// </summary>
        /// <param name="filePath"></param>
        /// <param name="assembly"></param>
        /// <param name="performLogging"></param>
        /// <returns></returns>
        public static Sprite? GetLocalSprite(string filePath, Assembly assembly, bool performLogging = false)
        {
            //SharedData.Log($"Getting local sprite {spriteId}");
            string logPrefix = "GetSprite";
            string[] resources = assembly.GetManifestResourceNames();
            Logging.Log(logPrefix, $"All resources found: {string.Join(", ", resources)}", performLogging);

            using (Stream stream = assembly.GetManifestResourceStream(filePath))
            {
                // Convert stream to bytes
                byte[] bytes = new byte[stream.Length];
                _ = stream.Read(bytes, 0, bytes.Length);

                // Create texture from bytes
                Texture2D texture = new Texture2D(2, 2);
                texture.LoadImage(bytes, true);

                // Create sprite from texture
                return Sprite.Create(texture,
                                        new Rect(0, 0, texture.width, texture.height),
                                        new Vector2(0.5f, 0.5f));
            }
        }
    }
}