﻿using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using KKSFramework.Management;
using KKSFramework.TableData;
using UnityEngine;
using UnityEngine.UI; // using TMPro;

namespace KKSFramework.Localization
{
    public enum TargetGlobalTextCompType
    {
        /// <summary>
        /// 글로벌 텍스트를 표시하려는 컴포넌트가 UGUI Text임.
        /// </summary>
        UIText,

        /// <summary>
        /// 글로벌 텍스트를 표시하려는 컴포넌트가 TextMeshPro임.
        /// </summary>
        TMP,
    }

    [Serializable]
    public class TranslatedInfo
    {
        public TargetGlobalTextCompType targetGlobalTextCompType;

        private Graphic _targetText;

        public string Key;

        public string[] Args = new string[0];
        public object[] ToObjectArgs => Array.ConvertAll (Args, x => x.ToString ()).ToArray ();

        public string text
        {
            set
            {
                switch (targetGlobalTextCompType)
                {
                    case TargetGlobalTextCompType.TMP:
                        // ((TextMeshPro) _targetText).text = value;
                        break;


                    case TargetGlobalTextCompType.UIText:
                        ((Text) _targetText).text = value;
                        break;

                    default:
                        break;
                }
            }
        }

        public void SetTextComp (Graphic textComp)
        {
            if (textComp is null)
            {
                Debug.Log ("textComp is null");
                return;
            }

#if TextMeshPro
            // if (!(textComp is Text) && !(textComp is TextMeshPro))
            // {
            //     Debug.Log ($"textComp is not TextTypeComponent: {textComp.GetType ().Name}");
            //     return;
            // }
#endif

            _targetText = textComp;
        }
    }


    /// <summary>
    /// 글로벌 텍스트 관리 클래스.
    /// </summary>
    public class LocalizationTextManager : ManagerBase<LocalizationTextManager>
    {
        #region Fields & Property

        /// <summary>
        /// 글로벌 텍스트 언어 타입.
        /// </summary>
        private int _languageType;

        public int LanguageType => _languageType;

        /// <summary>
        /// 글로벌 텍스트 언어 넘버.
        /// </summary>
        public int SelectedLanguage { get; private set; }

        /// <summary>
        /// 글로벌 텍스트 데이터.
        /// </summary>
        public Dictionary<string, LocalizingText> GlobalTextDict = new Dictionary<string, LocalizingText> ();

        /// <summary>
        /// 글로벌 텍스트 번역을 사용하고 있는 텍스트 컴포넌트.
        /// </summary>
        private readonly Dictionary<MonoBehaviour, TranslatedInfo> _translatedInfos =
            new Dictionary<MonoBehaviour, TranslatedInfo> ();

        /// <summary>
        /// 글로벌 텍스트 컴포넌트 클래스 리스트.
        /// </summary>
        private readonly List<LocalizingTextComponentBase> _globalTextComps = new List<LocalizingTextComponentBase> ();

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public async UniTask LoadGlobalTextData ()
        {
            GlobalTextDict =
                (await ReadCSVData.Instance.LoadCSVData<LocalizingText> ("Localization", nameof (LocalizingText)))
                .ToDictionary (x => x.Id, x => x);
        }


        /// <summary>
        /// 언어가 변경됨.
        /// </summary>
        public void ChangeLanguage (int p_num)
        {
            SelectedLanguage = p_num;
            _languageType = SelectedLanguage;
            ChangeGlobalText ();
        }


        /// <summary>
        /// 글로벌 텍스트 변경.
        /// </summary>
        private void ChangeGlobalText ()
        {
            _globalTextComps.ForEach (x => x.ChangeText ());
            _translatedInfos.Foreach (x =>
            {
                x.Value.text =
                    string.Format (
                        GlobalTextDict[x.Value.Key].GlobalTexts[_languageType],
                        x.Value.ToObjectArgs);
            });
        }


        /// <summary>
        /// 글로벌 텍스트 컴포넌트를 추가.
        /// </summary>
        public void RegistGlobalText (LocalizingTextComponentBase pLocalizingTextComponent)
        {
            if (_globalTextComps.Contains (pLocalizingTextComponent) == false)
                _globalTextComps.Add (pLocalizingTextComponent);
        }


        /// <summary>
        /// 글로벌 텍스트 등록. 
        /// </summary>
        public void RegistTranslate (TargetGlobalTextCompType targetCompType, string key, Graphic textComp,
            params object[] args)
        {
            if (textComp is null)
            {
                Debug.Log ("textComp is null");
                return;
            }

#if TextMeshPro
            if (!(textComp is Text) && !(textComp is TextMeshPro))
            {
                Debug.Log ($"textComp is not TextTypeComponent: {textComp.GetType ().Name}");
                return;
            }
#endif

            if (!GlobalTextDict.ContainsKey (key)) return;
            if (!_translatedInfos.ContainsKey (textComp))
            {
                _translatedInfos.Add (textComp, new TranslatedInfo ());
            }

            var translatedInfo = _translatedInfos[textComp];
            translatedInfo.targetGlobalTextCompType = targetCompType;
            translatedInfo.Key = key;
            translatedInfo.Args = Array.ConvertAll (args, x => x.ToString ());
            translatedInfo.SetTextComp (textComp);
            translatedInfo.text = string.Format (
                GlobalTextDict[translatedInfo.Key].GlobalTexts[_languageType],
                translatedInfo.ToObjectArgs);
        }

        public void UnregistTranslatedTextComp (Graphic textComp)
        {
            if (!_translatedInfos.ContainsKey (textComp))
                return;

            _translatedInfos.Remove (textComp);
        }


        public string GetTranslatedString (string key, int globalLanguageType)
        {
            return GlobalTextDict.ContainsKey (key)
                ? GlobalTextDict[key].GlobalTexts[globalLanguageType]
                : string.Empty;
        }

        #endregion


        #region EventMethods

        #endregion
    }
}