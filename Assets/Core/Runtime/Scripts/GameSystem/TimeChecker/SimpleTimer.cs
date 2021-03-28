using System;
using UniRx;

namespace KKSFramework.GameSystem
{
    /// <summary>
    /// 타이머.
    /// </summary>
    public class SimpleTimer
    {
        #region Fields & Property
        
        public bool OnChecking => _onChecking;

#pragma warning disable CS0649

#pragma warning restore CS0649

        private IDisposable _disposable;

        private Action _onCompleteAction;

        private bool _onChecking;

        #endregion


        #region Methods

        public IDisposable StartTimer (float seconds, Action onCompleteAction)
        {
            var remainTime = 0f;
            _onChecking = true;
            _onCompleteAction = onCompleteAction;
            
            _disposable.DisposeSafe ();
            _disposable = Observable
                .Timer (TimeSpan.Zero, TimeSpan.FromSeconds (UnityEngine.Time.deltaTime))
                .TimeInterval ()
                .Subscribe (Next);

            return _disposable;
            
            // 시간 체크 처리.
            void Next (TimeInterval<long> interval)
            {
                remainTime += (float)interval.Interval.TotalSeconds;
                if (!(remainTime >= seconds)) return;
                _onChecking = false;
                _onCompleteAction?.Invoke ();
                _disposable.DisposeSafe ();
            }
        }
        
        public void Stop (bool complete = false)
        {
            _onChecking = false;
            _disposable.DisposeSafe ();

            if (complete)
                _onCompleteAction?.Invoke ();
        }

        #endregion


        #region EventMethods

        #endregion
    }
}