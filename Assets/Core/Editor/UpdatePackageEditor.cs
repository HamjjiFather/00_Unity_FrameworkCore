using UnityEditor;
using UnityEditor.PackageManager.Requests;
using UnityEditor.PackageManager;
using UnityEngine;

namespace KKSFramework.Editor
{
    public static class UpdatePackageEditor
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        private static AddRequest _request;

        private const string PackageLink =
            "https://github.com/HamjjiFather/00_Unity_FrameworkCore.git?path=Assets/Core";

        private const string PackageId = "com.rhkdtjq0390.baseframe";

        private const string MenuItemPath = "Framework/Check for Updates Baseframe";
        
        #endregion


        #region Methods
        
        [MenuItem(MenuItemPath)]
        public static void CheckFrameworkPackage ()
        {
            _request = Client.Add (PackageLink);
            EditorApplication.update -= AddProgress;
        }

        
        /// <summary>
        /// 메뉴 아이템 버튼 조건 체크.
        /// 현재 프로젝트 BundleId와 BaseFrame패키지 아이디가 다르면 버튼을 활성화 한다.
        /// </summary>
        [MenuItem(MenuItemPath, validate = true)]
        public static bool CheckFrameworkValidate ()
        {
            return !Application.identifier.Equals (PackageId);
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