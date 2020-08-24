// using KKSFramework.Localization;
// using UnityEngine;
// using TMPro;
//
// namespace KKSFramework.GameSystem.LocalizingText
// {
//     /// <summary>
//     /// 변환될 텍스트를 가지고 있는 컴포넌트.
//     /// </summary>
//     [RequireComponent (typeof (TextMeshPro))]
//     public class LocalizingComponentTMP : LocalizingComponentBase<TextMeshPro>
//     {
//         #region Fields & Property
//
//         public override TextMeshPro TargetComponent => GetCachedComponent<TextMeshPro> ();
//
// #pragma warning disable CS0649
//
// #pragma warning restore CS0649
//
//         #endregion
//
//
//         #region UnityMethods
//
//         #endregion
//
//
//         #region EventMethods
//
//         #endregion
//
//
//         #region Methods
//
//         public override void ChangeText (string translatedString)
//         {
//             TargetComponent.text = translatedString;
//         }
//
//         #endregion
//     }
// }