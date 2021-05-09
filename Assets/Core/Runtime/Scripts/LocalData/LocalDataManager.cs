using System;
using System.IO;
using KKSFramework.Management;
using UnityEngine;

namespace KKSFramework.LocalData
{
    [Serializable]
    public class Bundle
    {
    }

    /// <summary>
    /// 보존이 필요한 게임 로컬 데이터 관리 클래스.
    /// </summary>
    public class LocalDataManager : ManagerBase<LocalDataManager>
    {
        #region Fields & Property

        private LocalDataComponent LocalDataComponent => ComponentBase as LocalDataComponent;

        public static string DataPath => Application.persistentDataPath;

        #endregion


        #region UnityMethods

        #endregion


        #region Methods


        public void SetSaveAction (Action saveAllAction)
        {
            LocalDataComponent.SetSaveAction (saveAllAction);
        }
        

        /// <summary>
        /// 게임 데이터 로드.
        /// </summary>
        public void LoadGameData (Bundle bundle)
        {
            bundle.FromJsonData ();
        }

        /// <summary>
        /// 게임 데이터 저장.
        /// </summary>
        /// .
        public void SaveGameData (Bundle bundle)
        {
            bundle.ToJsonData ();
        }


        public void DeleteData ()
        {
            var files = Directory.GetFiles (DataPath, "*.json");
            files.Foreach (File.Delete);
        }

        #endregion
    }


    public static class LocalDataExtension
    {
        /// <summary>
        /// Bundle 클래스 Json파일로 저장.
        /// </summary>
        public static void FromJsonData (this Bundle bundle)
        {
            var filePath = $"{LocalDataManager.DataPath}/{bundle.GetType ().Name}.json";
            if (!File.Exists (filePath)) return;
            var dataString = File.ReadAllText (filePath);
            JsonUtility.FromJsonOverwrite (dataString, bundle);
        }

        /// <summary>
        /// Json파일 Bundle클래스로 저장.
        /// </summary>
        public static void ToJsonData (this Bundle bundle)
        {
            var filePath = $"{LocalDataManager.DataPath}/{bundle.GetType ().Name}.json";
            File.WriteAllText (filePath, JsonUtility.ToJson (bundle));
        }
    }
}