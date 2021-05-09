using System;
using UniRx;

namespace KKSFramework.GameSystem
{
    public delegate void RemainingEventHandler (float remainSeconds);

    /// <summary>
    /// 경과 시간 확인.
    /// </summary>
    public class SimpleRemainTimeChecker
    {
        #region Fields & Property
        
        /// <summary>
        /// 남은 시간 이벤트 핸들러.
        /// </summary>
        private RemainingEventHandler _onRemaining;
        
        /// <summary>
        /// 완료시 실행할 이벤트 핸들러.
        /// </summary>
        private CompleteEventHandler _completed;
        
        private IDisposable _disposable;
        
        private bool _onChecking;

#pragma warning disable CS0649

#pragma warning restore CS0649
        
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

        public bool OnChecking => _onChecking;

        #endregion


        #region Methods

        public void StartRemain (float seconds)
        {
            var remainTime = seconds;
            _onChecking = true;
            
            _disposable.DisposeSafe ();
            _disposable = Observable
                .Timer (TimeSpan.Zero, TimeSpan.FromSeconds (UnityEngine.Time.deltaTime))
                .TimeInterval ()
                .Subscribe (Next, Complete);
            
            // 시간 체크 처리.
            void Next (TimeInterval<long> interval)
            {
                remainTime -= (float)interval.Interval.TotalSeconds;
                if (remainTime < 0)
                {
                    _onChecking = false;
                    _completed?.Invoke ();
                    _disposable.DisposeSafe ();
                    return;
                }
                
                _onRemaining?.Invoke (remainTime);
            }
            
            void Complete ()
            {
                _onChecking = false;
                _completed?.Invoke ();
                _disposable.DisposeSafe ();
            }
        }

        public void Stop (bool complete = false)
        {
            _onChecking = false;
            _disposable.DisposeSafe ();

            if (complete)
                _completed?.Invoke ();
        }

        #endregion


        #region EventMethods

        #endregion
    }
}