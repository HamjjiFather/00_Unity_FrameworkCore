using System;
using KKSFramework.Management;
using UniRx;
using UnityEngine;

namespace KKSFramework.InputEvent
{
    /// <summary>
    /// Managing to 'esc' button event on pc and android platforms.
    /// </summary>
    public sealed class EscapeEventManager : ManagerBase<EscapeEventManager>
    {
        /// <summary>
        /// normal esc action.
        /// </summary>
        private Action _escapeAction;

        /// <summary>
        /// hooked esc action.
        /// </summary>
        private Action _hookedEscapeAction;

        /// <summary>
        /// escape event action.
        /// </summary>
        public Action EscapeAction => _hookedEscapeAction ?? _escapeAction;


        public override void InitManager ()
        {
#if UNITY_ANDROID || UNITY_STANDALONE
            Observable.EveryUpdate ()
                .Where (x => Input.GetKeyDown (KeyCode.Escape))
                .Subscribe (_ =>
                {
                    EscapeAction.Invoke ();
                    if (_hookedEscapeAction is null)
                        return;

                    _hookedEscapeAction = null;
                });
#endif

            base.InitManager ();
        }
        

        /// <summary>
        /// change normal event action.
        /// </summary>
        [Obsolete("This method will be obsoleted. Therefore use SetEscapeEvent method instead of this")]
        public void AddEscapeEvent (Action action)
        {
#if UNITY_ANDROID || UNITY_STANDALONE
            _escapeAction = action;
#endif
        }
        
        
        /// <summary>
        /// change normal event action.
        /// </summary>
        public void SetEscapeEvent (Action action)
        {
#if UNITY_ANDROID || UNITY_STANDALONE
            _escapeAction = action;
#endif
        }

        
        /// <summary>
        /// hooking event action.
        /// </summary>
        public void SetHookingEscapeEvent (Action hookingAction)
        {
#if UNITY_ANDROID || UNITY_STANDALONE
            _hookedEscapeAction = hookingAction;
#endif
        }

        
        /// <summary>
        /// remove normal escape event.
        /// </summary>
        public void RemoveEscapeEvent ()
        {
#if UNITY_ANDROID || UNITY_STANDALONE
            _escapeAction = null;
#endif
        }
        
        
        /// <summary>
        /// remove hooking escape event.
        /// </summary>
        public void RemoveHookingEscapeEvent ()
        {
#if UNITY_ANDROID || UNITY_STANDALONE
            _hookedEscapeAction = null;
#endif
        }
    }
}