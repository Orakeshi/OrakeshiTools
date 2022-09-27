﻿using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Orakeshi.OrakeshiTools.Audio
{
    /// <summary>
    /// Handles everything relating to audio.
    /// </summary>
    public class AudioHandler
    {
        public AudioHandler(AudioSource audioSource)
        {
            AudioSource = audioSource;
        }
        public AudioSource AudioSource { get; }

        /// <summary>
        /// Plays sound with a given audio clip.
        /// </summary>
        /// <param name="audioClip">Audio clip to play.</param>
        /// <param name="isLooping">boolean value to determine if audio should loop.</param>
        /// <param name="volume">volume parameter.</param>
        public void PlaySound(AudioClip audioClip, bool isLooping = false, float volume = 1)
        {
            AudioSource.loop = isLooping;
            AudioSource.clip = audioClip;
            AudioSource.volume = volume;
            AudioSource.Play();
        }

        /// <summary>
        /// Plays sound asynchronously. Waits for sound to finish playing before returning.
        /// </summary>
        /// <param name="audioClip"></param>
        /// <returns></returns>
        public async UniTask<bool> PlaySoundAsync(AudioClip audioClip, bool isLooping = false, float volume = 1)
        {
            AudioSource.loop = isLooping;
            AudioSource.clip = audioClip;
            AudioSource.volume = volume;
            AudioSource.Play();
            
            await WaitForAudioEnd();
            return true;
        }
        
        /// <summary>
        /// Returns true when the audio has finished playing.
        /// </summary>
        /// <returns></returns>
        public async UniTask<bool> WaitForAudioEnd()
        {
            await UniTask.WaitUntil(() => AudioSource.isPlaying == false);
            return true;
        }
    }
}