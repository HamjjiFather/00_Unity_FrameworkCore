using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;
using UnityEngine;

namespace KKSFramework.Editor
{
    public static class UpdatePackageEditor
    {
        #region Fields & Property

        private static AddRequest _request;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region Methods
        
        [MenuItem("Framework/CheckForUpdatesPackage")]
        public static void CheckFrameworkPackage ()
        {
            _request = Client.Add ("https://github.com/HamjjiFather/00_Unity_FrameworkCore.git?path=Assets/Core");
            EditorApplication.update -= AddProgress;
        }
        
        
        private static void AddProgress()
        {
            if (!_request.IsCompleted) return;
            
            if (_request.Status == StatusCode.Success)
                Debug.Log("Installed: " + _request.Result.packageId);
            else if (_request.Status >= StatusCode.Failure)
                Debug.Log(_request.Error.message);

            EditorApplication.update -= AddProgress;
        }
        

        #endregion
    }
}