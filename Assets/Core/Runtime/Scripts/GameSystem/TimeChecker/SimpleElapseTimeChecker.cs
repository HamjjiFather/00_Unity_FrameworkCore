using System;
using UniRx;

namespace KKSFramework.GameSystem
{
    public delegate void CompleteEventHandler ();
    
    public delegate void ElapsedEventHandler (float elapsedSeconds);

    /// <summary>
    /// 경과 시간 확인.
    /// </summary>
    public class SimpleElapseTimeChecker
    {
        #region Fields & Property
        
        /// <summary>
        /// 경과 이벤트 핸들러.
        /// </summary>
        private ElapsedEventHandler _onElapsed;
        
        /// <summary>
        /// 완료시 실행할 이벤트 핸들러.
        /// </summary>
        private CompleteEventHandler _completed;
        
        private IDisposable _disposable;

        private bool _onChecking;

#pragma warning disable CS0649

#pragma warning restore CS0649
        
        public event ElapsedEventHandler Elapsed
        {
            add => _onElapsed += value;
            remove => _onElapsed -= value;
        }

        public event CompleteEventHandler Completed
        {
            add => _completed += value;
            remove => _completed -= value;
        }

        public bool OnChecking => _onChecking;

        #endregion


        #region Methods

        public void StartElapse (float seconds)
        {
            var remainTime = 0f;
            _onChecking = true;
            
            _disposable.DisposeSafe ();
            _disposable = Observable
                .Timer (TimeSpan.Zero, TimeSpan.FromSeconds (UnityEngine.Time.deltaTime))
                .TimeInterval ()
                .Subscribe (Next);
            
            // 시간 체크 처리.
            void Next (TimeInterval<long> interval)
            {
                remainTime += (float)interval.Interval.TotalSeconds;
                if (remainTime >= seconds)
                {
                    _onChecking = false;
                    _completed?.Invoke ();
                    _disposable.DisposeSafe ();
                    return;
                }
                
                _onElapsed?.Invoke (remainTime);
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