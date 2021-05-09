using System;
using UniRx;
using UnityEngine;

namespace KKSFramework.GameSystem
{
    public delegate void RemainingEventHandler (float remainSeconds);

    /// <summary>
    ///     경과 시간 확인.
    /// </summary>
    public class SimpleRemainTimeChecker
    {
        #region Fields & Property

        /// <summary>
        ///     남은 시간 이벤트 핸들러.
        /// </summary>
        private RemainingEventHandler _onRemaining;

        /// <summary>
        ///     완료시 실행할 이벤트 핸들러.
        /// </summary>
        private CompleteEventHandler _completed;

        private IDisposable _disposable;


        public event RemainingEventHandler Remaining
        {
            add => _onRemaining += value;
            remove => _onRemaining -= value;
        }

        private event CompleteEventHandler Completed
        {
            add => _completed += value;
            remove => _completed -= value;
        }

        public bool OnChecking { get; private set; }

        #endregion


        #region Methods

        public void StartRemain (float seconds)
        {
            var remainTime = seconds;
            OnChecking = true;

            _disposable.DisposeSafe ();
            _disposable = Observable
                .Timer (TimeSpan.Zero, TimeSpan.FromSeconds (Time.deltaTime))
                .TimeInterval ()
                .Subscribe (Next, Complete);

            // 시간 체크 처리.
            void Next (TimeInterval<long> interval)
            {
                remainTime -= (float) interval.Interval.TotalSeconds;
                if (remainTime < 0)
                {
                    OnChecking = false;
                    _completed?.Invoke ();
                    _disposable.DisposeSafe ();
                    return;
                }

                _onRemaining?.Invoke (remainTime);
            }

            void Complete ()
            {
                OnChecking = false;
                _completed?.Invoke ();
                _disposable.DisposeSafe ();
            }
        }

        public void Stop (bool complete = false)
        {
            OnChecking = false;
            _disposable.DisposeSafe ();

            if (complete)
                _completed?.Invoke ();
        }

        #endregion


        #region EventMethods

        #endregion
    }
}