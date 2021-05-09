using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace KKSFramework.Navigation
{
    public enum ViewEffectType
    {
        None,
        Fade,
        Move,
        Scale
    }

    [Serializable]
    public class ViewEffectModel
    {
        /// <summary>
        ///     effect animation duration.
        /// </summary>
        public float duration = 0.2f;

        /// <summary>
        ///     animation effect ease.
        /// </summary>
        public Ease effectEase = Ease.OutQuad;

        /// <summary>
        ///     animation effect type.
        /// </summary>
        public ViewEffectType viewEffectType = ViewEffectType.Fade;
    }

    [RequireComponent (typeof (CanvasGroup))]
    public class ViewEffector : CachedComponent
    {
        /// <summary>
        ///     show effect model.
        /// </summary>
        public ViewEffectModel showEffectModel;


        /// <summary>
        ///     hide effect model.
        /// </summary>
        public ViewEffectModel hideEffectModel;


        /// <summary>
        ///     is lock animation play.
        /// </summary>
        private bool _isLockPlayEffector;


        /// <summary>
        ///     canvasGroup.
        /// </summary>
        private CanvasGroup CanvasGroup => GetCachedComponent<CanvasGroup> ();


        /// <summary>
        ///     play fade-in view effect animation async.
        /// </summary>
        public async UniTask ShowAsync (CancellationToken ct = default)
        {
            if (_isLockPlayEffector)
                return;

            var tweenCore = CanvasGroup.DOFade (0, 0);

            if (showEffectModel.viewEffectType == ViewEffectType.Fade)
            {
                CanvasGroup.alpha = 0;

                tweenCore = CanvasGroup
                    .DOFade (1, showEffectModel.duration)
                    .SetEase (showEffectModel.effectEase);
                await tweenCore.WaitForCompleteAsync (ct);
            }

            if (showEffectModel.viewEffectType == ViewEffectType.Scale)
                await (tweenCore.target as Transform)
                    .DOScale (1f, showEffectModel.duration)
                    .SetEase (showEffectModel.effectEase)
                    .WaitForCompleteAsync (ct);

            if (showEffectModel.viewEffectType == ViewEffectType.Move)
                await (tweenCore.target as Transform)
                    .DOLocalMove (new Vector3 (0, 50f, 0), showEffectModel.duration)
                    .SetEase (showEffectModel.effectEase)
                    .From (true)
                    .WaitForCompleteAsync (ct);

            SetRaycast ();
        }


        /// <summary>
        ///     play fade-out view effect animation async.
        /// </summary>
        public async UniTask HideAsync (CancellationToken ct = default)
        {
            if (_isLockPlayEffector)
                return;

            var tweenCore = CanvasGroup.DOFade (1, hideEffectModel.duration);

            if (hideEffectModel.viewEffectType == ViewEffectType.Fade)
            {
                CanvasGroup.alpha = 1;
                tweenCore = CanvasGroup
                    .DOFade (0, hideEffectModel.duration)
                    .SetEase (hideEffectModel.effectEase);
                await tweenCore.WaitForCompleteAsync (ct);
            }

            if (hideEffectModel.viewEffectType == ViewEffectType.Scale)
                await (tweenCore.target as Transform)
                    .DOScale (0, hideEffectModel.duration)
                    .SetEase (hideEffectModel.effectEase)
                    .WaitForCompleteAsync (ct);

            if (hideEffectModel.viewEffectType == ViewEffectType.Move)
                await (tweenCore.target as Transform)
                    .DOLocalMove (new Vector3 (0, 50f, 0), hideEffectModel.duration)
                    .SetEase (hideEffectModel.effectEase)
                    .WaitForCompleteAsync (ct);

            SetRaycast ();
        }


        /// <summary>
        ///     play fade-in view effect animation immediately.
        /// </summary>
        public void ShowFadeImmediately ()
        {
            CanvasGroup.alpha = 1;
            SetRaycast ();
        }


        /// <summary>
        ///     play fade-out view effect animation immediately.
        /// </summary>
        public void HideFadeImmediately ()
        {
            CanvasGroup.alpha = 0;
            SetRaycast ();
        }


        /// <summary>
        ///     change effector playable state.
        /// </summary>
        public void SetLockState (bool isLockState)
        {
            _isLockPlayEffector = isLockState;
        }


        /// <summary>
        ///     change 'blockRaycasts' value of canvasGroup by canvas alpha value.
        /// </summary>
        private void SetRaycast ()
        {
            CanvasGroup.blocksRaycasts = CanvasGroup.alpha.Equals (1);
        }
    }
}