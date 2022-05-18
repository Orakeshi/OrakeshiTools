using Cysharp.Threading.Tasks;
using UnityEngine;

namespace Orakeshi.OrakeshiTools.Audio
{
    /// <summary>
    /// Handles everything relating to audio.
    /// </summary>
    public class AudioHandler
    {
        internal AudioSource AudioSource { get; set; }
        
        /// <summary>
        /// Plays sound with a given audio clip.
        /// </summary>
        /// <param name="audioClip"></param>
        public void PlaySound(AudioClip audioClip)
        {
            AudioSource.clip = audioClip;
            AudioSource.Play();
        }

        /// <summary>
        /// Plays sound asynchronously. Waits for sound to finish playing before returning.
        /// </summary>
        /// <param name="audioClip"></param>
        /// <returns></returns>
        public async UniTask<bool> PlaySoundAsync(AudioClip audioClip)
        {
            AudioSource.clip = audioClip;
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