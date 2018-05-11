#region File Description
//-----------------------------------------------------------------------------
// AudioManager.cs
//
// Microsoft XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------
#endregion

#region Using Statements
using System;
using System.IO;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Media;
#endregion

namespace XNAVideoJuego
{
    /// <summary>
    /// Component that manages audio playback for all sound effects.
    /// </summary>
    public class AudioManager : GameComponent
    {
        #region Singleton


        /// <summary>
        /// The singleton for this type
        /// </summary>
        private static AudioManager audioManager = null;


        #endregion


        #region Audio Data

        /// <summary>
        /// File list of all wav audio files
        /// </summary>
        private FileInfo[] effectFileList;
        private FileInfo[] soundtrackFileList;

        /// <summary>
        /// Content folder containing audio files
        /// </summary>
        private DirectoryInfo effectInfo;
        private DirectoryInfo soundtrackInfo;

        /// <summary>
        /// Collection of all loaded sound effects
        /// </summary>
        private static Dictionary<string, SoundEffect> effectList;

        /// <summary>
        /// Looping song used as the in-game soundtrack
        /// </summary>
        private static Dictionary<string, Song> soundtrackList;
        #endregion


        #region Initialization Methods

        /// <summary>
        /// Constructs the manager for audio playback of all sound effects.
        /// </summary>
        /// <param name="game">The game that this component will be attached to.</param>
        /// <param name="audioFolder">The directory containing audio files.</param>
        private AudioManager(Game game)
            : base(game)
        {
            try
            {
                effectInfo = new DirectoryInfo(game.Content.RootDirectory+"/Sonidos");
                soundtrackInfo = new DirectoryInfo(game.Content.RootDirectory + "/Sonidos/Soundtrack");
                effectFileList = effectInfo.GetFiles();
                soundtrackFileList = soundtrackInfo.GetFiles();
                effectList = new Dictionary<string, SoundEffect>();
                soundtrackList = new Dictionary<string, Song>();
                for (int i = 0; i < effectFileList.Length; i++)
                {
                    string effectName = Path.GetFileNameWithoutExtension(effectFileList[i].Name);
                    effectList[effectName] = game.Content.Load<SoundEffect>("Sonidos/"+ effectName);
                    effectList[effectName].Name = effectName;
                }
                for (int i = 0; i < soundtrackFileList.Length; i++)
                {
                    string songName = Path.GetFileNameWithoutExtension(soundtrackFileList[i].Name);
                    soundtrackList[songName] = game.Content.Load<Song>("Sonidos/Soundtrack/" + songName);
                }
            }
            catch (NoAudioHardwareException)
            {
                // silently fall back to silence
            }
        }

        public static void Initialize(Game game)
        {
            if (game == null)
                return;

            audioManager = new AudioManager(game);
            game.Components.Add(audioManager);
        }

        public static void PlaySoundtrack(string songName = "Soundtrack", bool looped = false)
        {
            if (audioManager == null || soundtrackList == null)
                return;
            if (soundtrackList.ContainsKey(songName))
            {
                MediaPlayer.IsRepeating = looped;
                MediaPlayer.Play(soundtrackList[songName]);
            }
        }

        public static void PauseSoundTrack()
        {
            if (MediaPlayer.State == MediaState.Playing)
                MediaPlayer.Pause();
        }

        public static void ResumeSoundTrack()
        {
            if (MediaPlayer.State == MediaState.Playing)
                MediaPlayer.Resume();
        }

        public static void StopSoundTrack()
        {
            if (MediaPlayer.State == MediaState.Playing)
                MediaPlayer.Stop();
        }

        #endregion


        #region Sound Play Methods

        /// <summary>
        /// Plays a fire-and-forget sound effect by name.
        /// </summary>
        /// <param name="soundName">The name of the sound to play.</param>
        public static void PlaySoundEffect(string soundName)
        {
            if (audioManager == null || effectList == null)
                return;

            if (effectList.ContainsKey(soundName))
            {
                effectList[soundName].Play();
            }
        }

        /// <summary>
        /// Plays a sound effect by name and returns an instance of that sound.
        /// </summary>
        /// <param name="soundName">The name of the sound to play.</param>
        /// <param name="looped">True if sound effect should loop.</param>
        /// <param name="instance">The SoundEffectInstance created for this sound effect.</param>
        public static void PlaySoundEffect(string soundName, bool looped, out SoundEffectInstance instance)
        {
            instance = null;
            if (audioManager == null || effectList == null)
                return;

            if (effectList.ContainsKey(soundName))
            {
                try
                {
                    instance = effectList[soundName].CreateInstance();
                    if (instance != null)
                    {
                        instance.IsLooped = looped;
                        instance.Play();
                    }
                }
                catch (InstancePlayLimitException)
                {
                    // silently fail (returns null instance) if instance limit reached
                }
            }
        }

        #endregion

    }
}
