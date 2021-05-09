using Cysharp.Threading.Tasks;
using UnityEngine;

namespace KKSFramework.Navigation
{
    public class ViewLayoutBase : MonoBehaviour
    {
        #region Fields & Property

        #endregion


        #region Methods

        public virtual void Initialize ()
        {
            gameObject.SetActive (false);
        }


        public virtual async UniTask ActiveLayout ()
        {
            gameObject.SetActive (true);
            await UniTask.CompletedTask;
        }


        public virtual async UniTask DisableLayout ()
        {
            gameObject.SetActive (false);
            await UniTask.CompletedTask;
        }

        #endregion


        #region EventMethods

        #endregion
    }
}