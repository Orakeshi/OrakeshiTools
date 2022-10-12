using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.Video;

namespace Orakeshi.OrakeshiTools.Video
{
    public class VideoHandler
    {
        public VideoHandler(VideoPlayer videoPlayer)
        {
            this.videoPlayer = videoPlayer;
            cancellationTokenSource = new CancellationTokenSource();
        }

        public bool IsPlaying { get; private set; }
        private VideoPlayer videoPlayer;
        private CancellationTokenSource cancellationTokenSource;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="videoClipToPlay"></param>
        /// <param name="isLooping"></param>
        public void Play(VideoClip videoClipToPlay, bool isLooping = false)
        {
            SetupVideoPlayer(videoClipToPlay, isLooping);
            videoPlayer.Play();
            
            IsPlaying = true;
        }

        /// <summary>
        /// Plays video asynchronously. Waits for video to finish playing before returning.
        /// </summary>
        /// <param name="videoClipToPlay"></param>
        /// <param name="isLooping"></param>
        /// <returns></returns>
        public async UniTask<bool> PlayVideoAsync(VideoClip videoClipToPlay, bool isLooping = false)
        {
            SetupVideoPlayer(videoClipToPlay, isLooping);
            videoPlayer.Play();
            
            await WaitForVideoEnd();
            return true;
        }

        private void SetupVideoPlayer(VideoClip videoClipToPlay, bool isLooping)
        {
            Stop();
            videoPlayer.clip = videoClipToPlay;
            videoPlayer.Prepare();
            videoPlayer.isLooping = isLooping;
        }
        
        /// <summary>
        /// Returns true when the video has finished playing.
        /// </summary>
        /// <returns></returns>
        public async UniTask<bool> WaitForVideoEnd()
        {
            cancellationTokenSource.CancelAfterSlim(TimeSpan.FromSeconds(500));
            try
            {
                await UniTask.WaitUntil(() => videoPlayer.isPrepared, cancellationToken: cancellationTokenSource.Token);
                await UniTask.WaitUntil(() => !videoPlayer.isPlaying, cancellationToken: cancellationTokenSource.Token);
            }
            catch (OperationCanceledException e)
            {
                Debug.Log(e.Message);
            }
            return true;
        }
        
        public void CancelVideo()
        {
            cancellationTokenSource.Cancel();
        }

        public void Stop()
        {
            videoPlayer.Stop();
            IsPlaying = false;
        }
    }
}