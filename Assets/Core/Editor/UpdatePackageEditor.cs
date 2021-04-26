using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;
using UnityEngine;

namespace KKSFramework.Editor
{
    public static class UpdatePackageEditor
    {
        #region Fields & Property

        private static SearchRequest SearchRequest;
        
        static AddRequest Request;

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region Methods
        
        [MenuItem("Framework/CheckPackage")]
        public static void CheckFrameworkPackage ()
        {
            Request = Client.Add ("https://github.com/HamjjiFather/00_Unity_FrameworkCore.git?path=Assets/Core");
            // SearchRequest =
            //     Client.Search ("https://github.com/HamjjiFather/00_Unity_FrameworkCore.git?path=Assets/Core");
        }
        
        
        private static void AddProgress()
        {
            if (Request.IsCompleted)
            {
                if (Request.Status == StatusCode.Success)
                    Debug.Log("Installed: " + Request.Result.packageId);
                else if (Request.Status >= StatusCode.Failure)
                    Debug.Log(Request.Error.message);

                EditorApplication.update -= AddProgress;
            }
        }
        
        
        private static void SearchProgress()
        {
            if (SearchRequest.IsCompleted)
            {
                if (Request.Status == StatusCode.Success)
                    Debug.Log("Installed: " + Request.Result.packageId);
                else if (Request.Status >= StatusCode.Failure)
                    Debug.Log(Request.Error.message);

                EditorApplication.update -= SearchProgress;
            }
        }

        #endregion


        #region EventMethods

        #endregion
    }
}