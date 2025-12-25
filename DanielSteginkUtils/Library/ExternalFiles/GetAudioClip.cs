using DanielSteginkUtils.Loggers;
using System.IO;
using System.Reflection;
using UnityEngine;
using WavLib;

namespace DanielSteginkUtils.ExternalFiles
{
    /// <summary>
    /// Gets external files and converts them to AudioClips
    /// </summary>
    public static class GetAudioClip
    {
        /// <summary>
        /// Gets a .WAV file from the embedded resources of the given assembly and converts it to an AudioClip
        /// </summary>
        /// <param name="assemblyName">Name of your mod's assembly. Most likely the name of the project (ie. DanielSteginkUtils)</param>
        /// <param name="fileName">Name of the file (ie. Assembly.Resources.File_Name.wav)</param>
        /// <param name="performLogging">Whether or not to log the process of getting the file</param>
        /// <returns></returns>
        public static AudioClip? GetAudioClipFromAssembly(string assemblyName, string fileName, 
                                                            bool performLogging = false)
        {
            string logPrefix = "GetAudioClipFromAssembly";
            Logging.Log(logPrefix, $"Getting {fileName} from assembly", performLogging);

            // Get the app's assembly
            Assembly assembly = Assembly.Load(assemblyName);
            if (assembly == null)
            {
                Logging.Log(logPrefix, $"Assembly '{assemblyName}' not found", performLogging);
                return null;
            }

            return GetAudioClipFromAssembly(assembly, fileName, performLogging);
        }

        /// <summary>
        /// Gets a .WAV file from the embedded resources of the given assembly and converts it to an AudioClip
        /// </summary>
        /// <param name="assembly">The assembly where the embedded resources are kept</param>
        /// <param name="fileName">Name of the file (ie. Assembly.Resources.File_Name.wav)</param>
        /// <param name="performLogging">Whether or not to log the process of getting the file</param>
        /// <returns></returns>
        public static AudioClip? GetAudioClipFromAssembly(Assembly assembly, string fileName,
                                                            bool performLogging = false)
        {
            // Get file as a resource stream
            using (Stream stream = assembly.GetManifestResourceStream($"{fileName}"))
            {
                if (stream == null)
                {
                    string logPrefix = "GetAudioClipFromAssembly";
                    Logging.Log(logPrefix, $"Embedded resource '{fileName}' not found", performLogging);
                    return null;
                }

                return GetAudioClipFromStream(stream, fileName, performLogging);
            }
        }

        /// <summary>
        /// Converts a given stream into an AudioClip
        /// </summary>
        /// <param name="stream"></param>
        /// <param name="clipName"></param>
        /// <param name="performLogging"></param>
        /// <returns></returns>
        public static AudioClip? GetAudioClipFromStream(Stream stream, string clipName, bool performLogging = false)
        {
            string logPrefix = "GetAudioClipFromStream";

            // Parse the stream into WAV data
            WavData wavData = new WavData();
            if (!wavData.Parse(stream))
            {
                Logging.Log(logPrefix, $"Unable to parse '{clipName}' into WAV data", performLogging);
                return null;
            }

            // Convert the data to an audio clip
            float[] samples = wavData.GetSamples();
            int sampleLength = samples.Length / wavData.FormatChunk.NumChannels;
            int channels = wavData.FormatChunk.NumChannels;
            int frequency = (int)wavData.FormatChunk.SampleRate;
            AudioClip audioClip = AudioClip.Create(clipName, sampleLength, channels, frequency, false);
            audioClip.SetData(samples, 0);
            UnityEngine.GameObject.DontDestroyOnLoad(audioClip);

            Logging.Log(logPrefix, $"'{clipName}' converted to AudioClip", performLogging);
            return audioClip;
        }
    }
}
