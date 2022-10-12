using Cysharp.Threading.Tasks;
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
        public void PlaySound(AudioClip audioClip, bool isLooping = false)
        {
            SetupAudioPlayer(audioClip, isLooping);
            AudioSource.Play();
        }

        /// <summary>
        /// Plays sound asynchronously. Waits for sound to finish playing before returning.
        /// </summary>
        /// <param name="audioClip"></param>
        /// <param name="isLooping"></param>
        /// <returns></returns>
        public async UniTask<bool> PlaySoundAsync(AudioClip audioClip, bool isLooping = false)
        {
            SetupAudioPlayer(audioClip, isLooping);
            AudioSource.Play();
            
            await WaitForAudioEnd();
            return true;
        }

        private void SetupAudioPlayer(AudioClip audioClip, bool isLooping)
        {
            AudioSource.loop = isLooping;
            AudioSource.clip = audioClip;
        }

        /// <summary>
        /// Resets and prepares the audio player for additional use.
        /// </summary>
        public void Prepare(bool isLooping = false, float volume = 1f)
        {
            AudioSource.Stop();
            AudioSource.loop = isLooping;
            AudioSource.volume = volume;
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