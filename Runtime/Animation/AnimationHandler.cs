using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Orakeshi.OrakeshiTools.Animation
{
    /// <summary>
    /// Animation handler with common animation features.
    /// </summary>
    public class AnimationHandler
    {
        public AnimationHandler(Animator animator)
        {
            Animator = animator;
        }

        public int AnimationLevel { get; set; }
        public Animator Animator { get; }

        private AnimatorStateInfo animatorStateInfo;

        /// <summary>
        /// Returns string value of animation with a given animation enum state.
        /// </summary>
        /// <param name="animationType"></param>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        protected virtual string GetAnimation<T>(T animationType) where T : Enum
        {
            return null;
        }

        /// <summary>
        /// Plays an animation with a given time delay.
        /// </summary>
        /// <param name="targetAnimation"></param>
        /// <param name="timeDelay"></param>
        public async UniTask AnimateWithDelay<T>(T targetAnimation, int timeDelay) where T : Enum
        {
            await UniTask.Delay(TimeSpan.FromSeconds(timeDelay), ignoreTimeScale: false);
            Animator.Play(GetAnimation(targetAnimation));
        }

        /// <summary>
        /// Method handles playing an animation on BIO.
        /// </summary>
        /// <param name="targetAnimation"></param>
        /// <param name="transitionDuration"></param>
        public virtual void Animate<T>(T targetAnimation, float transitionDuration = 0.2f) where T : Enum
        {
            string animation = GetAnimation(targetAnimation);
            Animator.CrossFade(animation, transitionDuration, 0);
        }

        public virtual async UniTask AnimateAsync<T>(T targetAnimation, int stringToHasBoolToListen) where T : Enum
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfterSlim(TimeSpan.FromSeconds(20));

            Animator.SetBool(stringToHasBoolToListen, true);
            Animate(targetAnimation);
            await WaitForAnimationEnd(stringToHasBoolToListen);
        }

        /// <summary>
        /// Responsible for waiting for animation end.
        /// </summary>
        public async UniTask<bool> WaitForAnimationEnd(int stringToHashBoolToListen)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfterSlim(TimeSpan.FromSeconds(20));

            await UniTask.WaitUntil(() => Animator.GetBool(stringToHashBoolToListen) == false, cancellationToken: cancellationTokenSource.Token);
            return true;
        }

        /// <summary>
        /// Returns bool value representing if animation is playing.
        /// </summary>
        /// <param name="animationToCheck"></param>
        /// <returns></returns>
        public bool IsAnimationPlaying<T>(params T[] animationToCheck) where T : Enum
        {
            animatorStateInfo = Animator.GetCurrentAnimatorStateInfo(0);
            foreach (T animationState in animationToCheck)
            {
                if (!animatorStateInfo.IsName(GetAnimation(animationState))) continue;
                return true;
            }
            return false;
        }

        /// <summary>
        /// Uses linear interpolation w/DoTween to move item to a target location.
        /// </summary>
        /// <param name="itemToLerp"></param>
        /// <param name="targetLocation"></param>
        public async UniTask<bool> LerpItem(GameObject itemToLerp, Vector3 targetLocation, float timeToLerp, Vector3 lerpScale)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            cancellationTokenSource.CancelAfterSlim(TimeSpan.FromSeconds(20));

            bool complete = false;
            // Move item to destination.
            itemToLerp.transform
                .DOLocalMove(targetLocation, timeToLerp)
                .OnComplete(() =>
                {
                    complete = true;
                });
            itemToLerp.transform
                .DOScale(lerpScale, timeToLerp);
            await UniTask.WaitUntil(() => complete, cancellationToken: cancellationTokenSource.Token);
            complete = false;
            return true;
        }
    }
}
