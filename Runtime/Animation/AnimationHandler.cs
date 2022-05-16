using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace Orakeshi.OrakeshiTools
{
    /// <summary>
    /// Animation handler with common animation features.
    /// </summary>
    public class AnimationHandler : MonoBehaviour
    {
        internal int AnimationLevel { get; set; }
        internal Animator Animator { get; set; }

        /// <summary>
        /// Responsible for waiting for animation end.
        /// </summary>
        public async UniTask<bool> WaitForAnimationEnd(int stringToHashBoolToListen)
        {
            await UniTask.WaitUntil(() => Animator.GetBool(stringToHashBoolToListen) == false);
            return true;
        }
        
        /// <summary>
        /// Uses linear interpolation w/DoTween to move item to a target location.
        /// </summary>
        /// <param name="itemToLerp"></param>
        /// <param name="targetLocation"></param>
        public async UniTask<bool> LerpItem(GameObject itemToLerp, Transform targetLocation, float timeToLerp, Vector3 lerpScale)
        {
            bool complete = false;
            // Move item to destination.
            itemToLerp.transform
                .DOLocalMove(targetLocation.position, timeToLerp)
                .OnComplete(() =>
                {
                    GameObject.Destroy(itemToLerp);
                    complete = true;
                });
            itemToLerp.transform
                .DOScale(lerpScale, timeToLerp);

            await UniTask.WaitUntil(() => complete);
            complete = false;
            return true;
        }
    }
}
