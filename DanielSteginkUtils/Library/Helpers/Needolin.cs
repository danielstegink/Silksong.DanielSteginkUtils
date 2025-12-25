using DanielSteginkUtils.Loggers;
using HarmonyLib;
using HutongGames.PlayMaker;
using HutongGames.PlayMaker.Actions;
using UnityEngine;

namespace DanielSteginkUtils.Helpers
{
    /// <summary>
    /// Library for interacting with the Needolin FSM
    /// </summary>
    [HarmonyPatch(typeof(Fsm), "Awake")]
    public static class Needolin
    {
        /// <summary>
        /// The Needolin FSM
        /// </summary>
        public static Fsm? NeedolinFsm { get; private set; } = null;

        /// <summary>
        /// The default AudioClip used by the Needolin
        /// </summary>
        public static AudioClip? DefaultClip { get; private set; } = null;

        /// <summary>
        /// The replacement default AudioClip used by the Needolin
        /// </summary>
        public static AudioClip? NewDefaultClip { get; private set; } = null;

        /// <summary>
        /// The current AudioClip used by the Needolin
        /// </summary>
        public static AudioClip? CurrentClip { get; private set; } = null;

        /// <summary>
        /// Gets the Needolin FSM and stores it in the NeedolinFsm property
        /// </summary>
        /// <param name="__instance"></param>
        [HarmonyPostfix]
        private static void GetNeedolinFsm(Fsm __instance)
        {
            // Verify the FSM name
            if (!__instance.name.Equals("FSM"))
            {
                return;
            }

            // Verify the game object
            if (!__instance.GameObjectName.Equals("Hero_Hornet(Clone)"))
            {
                return;
            }

            // Verify a unique state
            FsmState? state = GetNeedolinState(__instance);
            if (state == null)
            {
                return;
            }

            NeedolinFsm = __instance;
            CacheDefaultAudioClip(NeedolinFsm);
        }

        /// <summary>
        /// Gets the FSM state "Start Needolin Proper", which contains the action that triggers the Needolin's music
        /// </summary>
        /// <param name="needolinFsm"></param>
        /// <returns></returns>
        public static FsmState? GetNeedolinState(Fsm needolinFsm)
        {
            FsmState? state = needolinFsm.GetState("Start Needolin Proper");
            if (state == default)
            {
                state = null;
            }

            return state;
        }

        /// <summary>
        /// Gets the default AudioClip for the Needolin and stores it in the DefaultClip property
        /// </summary>
        /// <param name="needolinFsm"></param>
        /// <param name="performLogging"></param>
        private static void CacheDefaultAudioClip(Fsm needolinFsm, bool performLogging = false)
        {
            FsmState? state = GetNeedolinState(needolinFsm);
            if (state == null)
            {
                Logging.Log("CacheDefaultAudioClip", "Error getting FSM state", performLogging);
                return;
            }

            StartNeedolinAudioLoop action = (StartNeedolinAudioLoop)state.Actions[6];
            DefaultClip = (AudioClip)action.DefaultClip.Value;
            Logging.Log("CacheDefaultAudioClip", $"Default clip set: {DefaultClip.name}", performLogging);
        }

        /// <summary>
        /// Assigns a new AudioClip to the NewDefaultClip property, where it can be used as the new default
        /// clip for theNeedolin
        /// </summary>
        /// <param name="audioClip"></param>
        public static void SetNewDefaultClip(AudioClip audioClip)
        {
            NewDefaultClip = audioClip;
        }

        /// <summary>
        /// Assigns a new AudioClip to the Needolin, which is also stored in the CurrentClip property
        /// </summary>
        /// <param name="needolinFsm"></param>
        /// <param name="audioClip"></param>
        /// <param name="performLogging"></param>
        public static void SetNewAudioClip(Fsm needolinFsm, AudioClip audioClip, bool performLogging = false)
        {
            FsmState? state = GetNeedolinState(needolinFsm);
            if (state == null)
            {
                Logging.Log("SetNewAudioClip", "Error getting FSM state", performLogging);
                return;
            }

            StartNeedolinAudioLoop action = (StartNeedolinAudioLoop)state.Actions[6];
            action.DefaultClip.value = audioClip;
            CurrentClip = (AudioClip)action.DefaultClip.value;
            Logging.Log("SetNewAudioClip", $"New clip set: {CurrentClip.name}", performLogging);
        }

        /// <summary>
        /// Resets the Needolin to use the "default" clip
        /// </summary>
        /// <param name="needolinFsm"></param>
        /// <param name="useNewDefault">If true, use NewDefaultClip instead of DefaultClip</param>
        /// <param name="performLogging"></param>
        public static void ResetAudioClip(Fsm needolinFsm, bool useNewDefault, bool performLogging = false)
        {
            FsmState? state = GetNeedolinState(needolinFsm);
            if (state == null)
            {
                Logging.Log("ResetAudioClip", "Error getting FSM state", performLogging);
                return;
            }

            StartNeedolinAudioLoop action = (StartNeedolinAudioLoop)state.Actions[6];
            action.DefaultClip.value = GetDefaultClip(useNewDefault);
            CurrentClip = (AudioClip)action.DefaultClip.value;
            Logging.Log("ResetAudioClip", $"Clip reset: {CurrentClip.name}", performLogging);
        }

        /// <summary>
        /// Gets the default AudioClip for the Needolin
        /// </summary>
        /// <param name="useNewDefault">If true, use NewDefaultClip instead of DefaultClip</param>
        /// <returns></returns>
        public static AudioClip? GetDefaultClip(bool useNewDefault)
        {
            if (useNewDefault &&
                NewDefaultClip != null)
            {
                return NewDefaultClip;
            }
            else
            {
                return DefaultClip;
            }
        }
    }
}