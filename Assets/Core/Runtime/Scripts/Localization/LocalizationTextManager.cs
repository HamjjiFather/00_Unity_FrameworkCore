using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using KKSFramework.Management;
using KKSFramework.TableData;
using UniRx;

namespace KKSFramework.Localization
{
    /// <summary>
    ///     글로벌 텍스트 관리 클래스.
    /// </summary>
    public class LocalizationTextManager : ManagerBase<LocalizationTextManager>
    {
        #region Fields & Property

        /// <summary>
        ///     글로벌 텍스트 언어 넘버.
        /// </summary>
        public int SelectedLanguage { get; private set; }

        /// <summary>
        ///     언어 변경 커맨드.
        /// </summary>
        public readonly ReactiveCommand LanguageChangeCommand = new ReactiveCommand ();


        /// <summary>
        ///     글로벌 텍스트 데이터.
        /// </summary>
        public Dictionary<string, LocalizingText> LocalizingTexts = new Dictionary<string, LocalizingText> ();

        #endregion


        #region UnityMethods

        #endregion


        #region Methods

        public async UniTask LoadGlobalTextData ()
        {
            LocalizingTexts =
                (await ReadCSVData.Instance.LoadCSVData<LocalizingText> ("Localization", nameof (LocalizingText)))
                .ToDictionary (x => x.Id, x => x);
        }


        /// <summary>
        ///     언어가 변경됨.
        /// </summary>
        public void ChangeLanguage (int language)
        {
            SelectedLanguage = language;
            LanguageChangeCommand.Execute ();
        }


        /// <summary>
        ///     번역된 문자열 리턴.
        /// </summary>
        public string GetTranslatedString (string key, params object[] args)
        {
            return LocalizingTexts.ContainsKey (key)
                ? string.Format (LocalizingTexts[key].LocalizationItems[SelectedLanguage], args)
                : string.Empty;
        }


        /// <summary>
        ///     번역된 문자열 리턴.
        /// </summary>
        public string GetTranslatedString (int globalLanguageType, string key, params object[] args)
        {
            return LocalizingTexts.ContainsKey (key)
                ? string.Format (LocalizingTexts[key].LocalizationItems[globalLanguageType], args)
                : string.Empty;
        }

        #endregion


        #region EventMethods

        #endregion
    }
}