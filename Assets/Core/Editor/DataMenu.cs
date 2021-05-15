using System.Diagnostics;
using KKSFramework.LocalData;
using UnityEditor;
using UnityEngine;

namespace KKSFramework.Editor
{
    public static class DataFolderEditor
    {
        [MenuItem ("Framework/Folder/Open Temporary Cache Folder", priority = 3)]
        public static void ShowTemporaryCacheFolder ()
        {
            Process.Start (Application.temporaryCachePath);
        }

        [MenuItem ("Framework/Folder/Open Persistent Data Folder", priority = 2)]
        public static void ShowPersistentFolder ()
        {
            Process.Start (Application.persistentDataPath);
        }


        [MenuItem ("Framework/Folder/Open Data Path", priority = 1)]
        public static void ShowMainFolder ()
        {
            Process.Start (Application.dataPath);
        }
        
        
        [MenuItem ("Framework/Data/Delete Persistent Data")]
        public static void DeleteLocalData ()
        {
            PlayerPrefs.DeleteAll ();
            LocalDataManager.Instance.DeleteData ();
        }
    }
}