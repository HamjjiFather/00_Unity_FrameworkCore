using System;
using KKSFramework.Management;

namespace KKSFramework.LocalData
{
    public class LocalDataComponent : ComponentBase
    {
        #region Fields & Property

        private Action _saveAction;

        #endregion


        #region UnityMethods

        /// <summary>
        ///     앱을 종료할 경우 데이터를 저장.
        /// </summary>
        private void OnApplicationQuit ()
        {
            _saveAction?.Invoke ();
        }

        #endregion


        #region Methods

        public void SetSaveAction (Action action)
        {
            _saveAction = action;
        }

        #endregion


        #region Constructor

        #endregion


        #region EventMethods

        #endregion
    }
}