using System;
using UniRx;
using UnityEngine;

namespace KKSFramework.Localization
{
    /// <summary>
    ///     변환될 텍스트를 가지고 있는 컴포넌트.
    /// </summary>
    public abstract class LocalizingComponentBase<T> : CachedComponent where T : Component
    {
        #region UnityMethods

        protected virtual void OnEnable ()
        {
            if (!isAutoTranslate) return;
            SubscribeLocalizingData ();
        }

        #endregion


        #region Fields & Property

        public bool isAutoTranslate;

        [Tooltip ("컴포넌트로 사용할 경우 Key값은 무조건 채워져 있어야 한다.")]
        public LocalizationModel localizationModel;

        /// <summary>
        ///     대상 컴포넌트.
        /// </summary>
        public virtual T TargetComponent { get; }

        /// <summary>
        ///     언어 변경 구독.
        /// </summary>
        private IDisposable _subscribeTranslate;

        #endregion


        #region EventMethods

        #endregion


        #region Methods

        /// <summary>
        ///     컴포넌트 세팅.
        /// </summary>
        protected void SubscribeLocalizingData ()
        {
            _subscribeTranslate = LocalizationTextManager.Instance.LanguageChangeCommand
                .TakeUntilDisable (this)
                .DoOnSubscribe (SubscribeChangeText)
                .Subscribe (_ => { SubscribeChangeText (); });

            void SubscribeChangeText ()
            {
                var translatedString =
                    LocalizationTextManager.Instance.GetTranslatedString (localizationModel.key,
                        localizationModel.ToObjectArgs);
                ChangeText (translatedString);
            }
        }


        /// <summary>
        ///     텍스트 변경.
        /// </summary>
        protected abstract void ChangeText (string text);

        #endregion
    }
}