using System;
using UniRx;
using UnityEngine;

namespace KKSFramework.GameSystem
{
    /// <summary>
    ///     타이머.
    /// </summary>
    public class SimpleTimer
    {
        #region Fields & Property

        public bool OnChecking { get; private set; }


        private IDisposable _disposable;

        private Action _onCompleteAction;

        #endregion


        #region Methods

        public IDisposable StartTimer (float seconds, Action onCompleteAction)
        {
            var remainTime = 0f;
            OnChecking = true;
            _onCompleteAction = onCompleteAction;

            _disposable.DisposeSafe ();
            _disposable = Observable
                .Timer (TimeSpan.Zero, TimeSpan.FromSeconds (Time.deltaTime))
                .TimeInterval ()
                .Subscribe (Next);

            return _disposable;

            // 시간 체크 처리.
            void Next (TimeInterval<long> interval)
            {
                remainTime += (float) interval.Interval.TotalSeconds;
                if (!(remainTime >= seconds)) return;
                OnChecking = false;
                _onCompleteAction?.Invoke ();
                _disposable.DisposeSafe ();
            }
        }

        public void Stop (bool complete = false)
        {
            OnChecking = false;
            _disposable.DisposeSafe ();

            if (complete)
                _onCompleteAction?.Invoke ();
        }

        #endregion


        #region EventMethods

        #endregion
    }
}