using System;
using System.Collections.Generic;
using DG.Tweening;
using KKSFramework.GameSystem;
using KKSFramework.Sound;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace KKSFramework.UI
{
    public delegate void HoldingCancelHandler ();
    
    
    /// <summary>
    /// 버튼 베이스 클래스.
    /// </summary>
    public class ButtonExtension : Button
    {
        #region EventMethods

        /// <summary>
        /// 클릭 이벤트.
        /// </summary>
        protected virtual void OnClick ()
        {
            if (soundTypeEnum != SoundTypeEnum.None) SoundPlayHelper.PlaySfx (soundTypeEnum);
        }

        #endregion


        #region Fields & Property

        #region Base
        
        /// <summary>
        /// 사운드 타입.
        /// </summary>
        public SoundTypeEnum soundTypeEnum = SoundTypeEnum.sfx_button;

        /// <summary>
        /// 버튼 텍스트.
        /// </summary>
        public Text buttonText;
        
        #endregion
        
        
        #region HoldingAction

        public bool useHoldingAction;

        public float defaultHoldDuration = 2f;

        public bool OnHolding { get; private set; }

        public SimpleRemainTimeChecker RemainTimeChecker { get; private set; } = new SimpleRemainTimeChecker ();

        public UnityEvent cancelEvent;

        private readonly List<IDisposable> _disposables = new List<IDisposable> ();

        private bool _callHoldingComplete;

        #endregion


        #region ScaleEffect

        public bool useScaleEffect;

        public float scaleEffectDuration = 0.1f;

        public Vector3 toScaleValue = new Vector3 (1.1f, 1.1f, 1.1f);

        private bool _onScaleEffect;

        private Vector3 _originScaleValue;

        #endregion
        
        #endregion


        #region UnityMethods

        protected override void Awake ()
        {
            base.Awake ();
            _originScaleValue = transform.localScale;
            RemainTimeChecker.Completed += CompleteRemainingCheck;
        }
        

        protected new void Reset ()
        {
            targetGraphic = GetComponentInChildren<Graphic> ();
        }
        

        protected override void Start ()
        {
            base.Start ();
            onClick.AddListener (OnClick);
        }


        protected override void OnDisable ()
        {
            base.OnDisable ();
            
        }
        
        
        public override void OnPointerDown (PointerEventData eventData)
        {
            base.OnPointerDown (eventData);

            if (useHoldingAction)
            {
                OnHolding = true;
                _callHoldingComplete = false;
                RemainTimeChecker.StartRemain (defaultHoldDuration);
            }

            if (useScaleEffect)
            {
                _onScaleEffect = true;
                transform.DOScale (toScaleValue, scaleEffectDuration);
            }
        }


        public override void OnPointerExit (PointerEventData eventData)
        {
            base.OnPointerExit (eventData);

            if (OnHolding)
            {
                OnHolding = false;
                _callHoldingComplete = false;
                RemainTimeChecker.Stop ();
                cancelEvent.Invoke ();    
            }

            if (!useScaleEffect) return;
            if (!_onScaleEffect) return;
            _onScaleEffect = true;
            transform.DOScale (_originScaleValue, scaleEffectDuration);
        }


        public override void OnPointerClick (PointerEventData eventData)
        {
            if (useScaleEffect)
            {
                if (_onScaleEffect)
                {
                    _onScaleEffect = false;
                    transform.DOScale (_originScaleValue, scaleEffectDuration);
                }
            }
            
            if (useHoldingAction)
            {
                if (OnHolding)
                {
                    if (!_callHoldingComplete)
                    {
                        OnHolding = false;
                        _callHoldingComplete = false;
                        RemainTimeChecker.Stop ();
                        cancelEvent.Invoke ();
                        base.OnPointerClick (eventData);
                        return;
                    }

                    return;
                }

                if (_callHoldingComplete)
                {
                    return;
                }
            }

            base.OnPointerClick (eventData);
        }


        public override void OnPointerUp (PointerEventData eventData)
        {
            if (useScaleEffect)
            {
                if (_onScaleEffect)
                {
                    _onScaleEffect = false;
                    transform.DOScale (_originScaleValue, scaleEffectDuration);
                }
            }
            
            if (useHoldingAction)
            {
                if (OnHolding)
                {
                    if (!_callHoldingComplete)
                    {
                        OnHolding = false;
                        _callHoldingComplete = false;
                        RemainTimeChecker.Stop ();
                        cancelEvent.Invoke ();
                        base.OnPointerUp (eventData);
                        return;
                    }

                    return;
                }

                if (_callHoldingComplete)
                {
                    return;
                }
            }

            base.OnPointerUp (eventData);
        }

        #endregion


        #region Methods

        /// <summary>
        /// 베이스 버튼에서 컴포넌트를 변경함.
        /// </summary>
        public void ReplaceComponent (Button targetButton)
        {
            interactable = targetButton.interactable;
            transition = targetButton.transition;
            colors = targetButton.colors;
            onClick = targetButton.onClick;
        }

        
        /// <summary>
        /// 버튼 텍스트 변경.
        /// </summary>
        public void SetText (string text)
        {
            if (buttonText != null)
                buttonText.text = text;
        }


        /// <summary>
        /// Add OnClick listener.
        /// </summary>
        public void AddListener (UnityAction call)
        {
            onClick.AddListener (call);
        }
        
        
        /// <summary>
        /// Remove OnClick listener.
        /// </summary>
        public void RemoveListener (UnityAction call)
        {
            onClick.RemoveListener (call);
        }
        
        
        /// <summary>
        /// Remove all OnClick listener.
        /// </summary>
        public void RemoveAllListeners ()
        {
            onClick.RemoveAllListeners ();
        }


        /// <summary>
        /// Complete holding action.
        /// </summary>
        private void CompleteRemainingCheck ()
        {
            if (!useHoldingAction)
                return;

            OnHolding = false;
            _callHoldingComplete = true;
        }


        /// <summary>
        /// Dispose.
        /// </summary>
        public void Dispose ()
        {
            _disposables.ForEach (d => d.DisposeSafe ());
        }

        #endregion
    }
}