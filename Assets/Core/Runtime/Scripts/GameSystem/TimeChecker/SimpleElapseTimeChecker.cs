using System;
using UniRx;
using UnityEngine;

namespace KKSFramework.GameSystem
{
    public delegate void CompleteEventHandler ();

    public delegate void ElapsedEventHandler (float elapsedSeconds);

    /// <summary>
    ///     경과 시간 확인.
    /// </summary>
    public class SimpleElapseTimeChecker
    {
        #region Fields & Property

        /// <summary>
        ///     경과 이벤트 핸들러.
        /// </summary>
        private ElapsedEventHandler _onElapsed;

        /// <summary>
        ///     완료시 실행할 이벤트 핸들러.
        /// </summary>
        private CompleteEventHandler _completed;

        private IDisposable _disposable;


        public event ElapsedEventHandler Elapsed
        {
            add => _onElapsed += value;
            remove => _onElapsed -= value;
        }

        private event CompleteEventHandler Completed
        {
            add => _completed += value;
            remove => _completed -= value;
        }

        public bool OnChecking { get; private set; }

        #endregion


        #region Methods

        public void StartElapse (float seconds)
        {
            var remainTime = 0f;
            OnChecking = true;

            _disposable.DisposeSafe ();
            _disposable = Observable
                .Timer (TimeSpan.Zero, TimeSpan.FromSeconds (Time.deltaTime))
                .TimeInterval ()
                .Subscribe (Next);

            // 시간 체크 처리.
            void Next (TimeInterval<long> interval)
            {
                remainTime += (float) interval.Interval.TotalSeconds;
                if (remainTime >= seconds)
                {
                    OnChecking = false;
                    _completed?.Invoke ();
                    _disposable.DisposeSafe ();
                    return;
                }

                _onElapsed?.Invoke (remainTime);
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