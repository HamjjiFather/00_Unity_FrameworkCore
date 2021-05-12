using System.Diagnostics;
using KKSFramework.LocalData;
using UnityEditor;
using UnityEngine;

namespace KKSFramework.Editor
{
    public static class DataFolderEditor
    {
        [MenuItem ("Framework/Folder/OpenCacheFolder", priority = 3)]
        public static void ShowTemporaryCacheFolder ()
        {
            Process.Start (Application.temporaryCachePath);
        }

        [MenuItem ("Framework/Folder/OpenPersistentFolder", priority = 2)]
        public static void ShowPersistentFolder ()
        {
            Process.Start (Application.persistentDataPath);
        }


        [MenuItem ("Framework/Folder/OpenDataPath", priority = 1)]
        public static void ShowMainFolder ()
        {
            Process.Start (Application.dataPath);
        }
        
        
        [MenuItem ("Framework/Data/Delete Local Data")]
        public static void DeleteLocalData ()
        {
            PlayerPrefs.DeleteAll ();
            LocalDataManager.Instance.DeleteData ();
        }
    }
}