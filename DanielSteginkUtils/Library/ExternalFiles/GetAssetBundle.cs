using DanielSteginkUtils.Loggers;
using System.IO;
using UnityEngine;

namespace DanielSteginkUtils.ExternalFiles
{
    /// <summary>
    /// Library for interacting with asset bundles
    /// </summary>
    public static class GetAssetBundle
    {
        /// <summary>
        /// Loads an asset bundle from the given path. This may not work with bundles in nested folders, such as scene bundles.
        /// 
        /// Asset bundles can be finicky about when you load them, so I recommend getting them when the GameManager wakes up.
        /// 
        /// Note that multiple apps loading the same bundle can cause errors, so make sure to use 
        /// the bundle's Unload function after you've acquired the assets you need.
        /// </summary>
        /// <param name="path">Path of the bundle file. 
        ///                     Do not include the root folder Hollow Knight Silksong_Data\StreamingAssets\aa\OS_FOLDER_NAME.
        ///                     </param>
        /// <param name="performLogging">Whether or not to log any errors that occur</param>
        /// <returns></returns>
        public static AssetBundle? GetBundle(string path, bool performLogging = false)
        {
            string rootFolder = Application.streamingAssetsPath;
            string osFolder = Application.platform switch
                                {
                                    RuntimePlatform.WindowsPlayer => "StandaloneWindows64",
                                    RuntimePlatform.OSXPlayer => "StandaloneOSX",
                                    RuntimePlatform.LinuxPlayer => "StandaloneLinux64",
                                    _ => ""
                                };
            string parentFolder = Path.Combine(rootFolder, "aa", osFolder);
            string fullPath = Path.Combine(parentFolder, path);
            AssetBundle bundle = AssetBundle.LoadFromFile(fullPath);

            if (bundle == null)
            {
                Logging.Log("GetBundle", $"Error loading bundle {path}", performLogging);
            }
            return bundle;
        }

        ///// <summary>
        ///// Gets a game object from a given scene.
        ///// 
        ///// This process uses Addressables to physically load a scene, so it is best to run this when the GameManager wakes up.
        ///// </summary>
        ///// <param name="objectName">Name of the object (ie. Pinstress Boss)</param>
        ///// <param name="sceneName">Name of the scene (ie. Peak_07)</param>
        ///// <param name="performLogging">Whether or not to log any errors that occur</param>
        ///// <returns></returns>
        //public static GameObject? GetObjectFromScene(string objectName, string sceneName, bool performLogging = false)
        //{
        //    GameObject? targetObject = null;

        //    // Use Addressables to load the target scene
        //    SceneInstance sceneInstance = Addressables.LoadSceneAsync($"Scenes/{sceneName}", LoadSceneMode.Additive).Result;
        //    Scene scene = sceneInstance.Scene;

        //    // Iterate through each object in the scene
        //    foreach (GameObject gameObject in scene.GetRootGameObjects())
        //    {
        //        Transform[] children = gameObject.GetComponentsInChildren<Transform>(true);
        //        Transform targetTransform = children.FirstOrDefault(x => x.gameObject.name.Equals(objectName));

        //        // If the target object is found, stop
        //        if (targetTransform != null)
        //        {
        //            targetObject = targetTransform.gameObject;
        //            break;
        //        }
        //    }

        //    // Unload the scene when we're done
        //    Addressables.UnloadSceneAsync(sceneInstance);

        //    return targetObject;
        //}
    }
}