using System;
using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.Localization
{
    /// <summary>
    /// 변환될 텍스트를 가지고 있는 컴포넌트.
    /// </summary>
    [RequireComponent (typeof (Text))]
    public class LocalizingComponentForText : LocalizingComponentBase<Text>
    {
        #region Fields & Property

        public override Text TargetComponent => GetCachedComponent<Text> ();

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        #endregion


        #region EventMethods

        #endregion


        #region Methods

        protected override void ChangeText (string translatedString)
        {
            TargetComponent.text = translatedString;
        }
        
        
        /// <summary>
        /// 글로벌 텍스트 데이터를 매뉴얼로 세팅.
        /// </summary>
        public void SetGlobalTextData (string keyName, params object[] args)
        {
            localizationModel.key = keyName;
            localizationModel.args = Array.ConvertAll (args, x => x.ToString ());
        }

        #endregion
    }
}