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
        private FileInfo[] audioFileList;

        /// <summary>
        /// Content folder containing audio files
        /// </summary>
        private DirectoryInfo audioFolder;
        
        /// <summary>
        /// Collection of all loaded sound effects
        /// </summary>
        private static Dictionary<string, SoundEffect> soundList;

        /// <summary>
        /// Looping song used as the in-game soundtrack
        /// </summary>
        private static Song soundtrack;

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
                audioFolder = new DirectoryInfo(game.Content.RootDirectory+"/Sonidos");
                audioFileList = audioFolder.GetFiles();
                soundList = new Dictionary<string, SoundEffect>();

                for (int i = 0; i < audioFileList.Length; i++)
                {
                    string soundName = Path.GetFileNameWithoutExtension(audioFileList[i].Name);
                    soundList[soundName] = game.Content.Load<SoundEffect>("Sonidos/"+ soundName);
                    soundList[soundName].Name = soundName;
                }

                soundtrack = game.Content.Load<Song>("Sonidos/Soundtrack/Soundtrack");
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

        public static void PlaySoundTrack()
        {
            if (soundtrack == null)
                return;

            MediaPlayer.Play(soundtrack);
        }

        public static void PaUseSoundTrack()
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
            if (audioManager == null || soundList == null)
                return;

            if (soundList.ContainsKey(soundName))
            {
                soundList[soundName].Play();
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
            if (audioManager == null || soundList == null)
                return;

            if (soundList.ContainsKey(soundName))
            {
                try
                {
                    instance = soundList[soundName].CreateInstance();
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
