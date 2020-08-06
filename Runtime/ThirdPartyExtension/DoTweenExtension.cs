using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;

#if CSHARP_7_OR_LATER || (UNITY_2018_3_OR_NEWER && (NET_STANDARD_2_0 || NET_4_6))
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

namespace KKSFramework
{
    public static class DoTweenAsyncTriggerExtensions
    {
        #region Special for single operation.

        public static UniTask<Tweener> WaitForCompleteAsync (this Tweener tweener,
            CancellationToken cancellationToken = default)
        {
            var completionSource = new UniTaskCompletionSource<Tweener> ();
            tweener.OnComplete (() =>
            {
                if (!cancellationToken.IsCancellationRequested) completionSource.TrySetResult (tweener);
            });
            return completionSource.Task;
        }

        #endregion
    }
}

#endif