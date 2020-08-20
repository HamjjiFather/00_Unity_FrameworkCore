using UnityEngine;

namespace KKSFramework.DataBind
{
    public class BindableComponents<T> : Bindable where T : Component
    {
        /// <summary>
        /// 타겟이 되는 컴포넌트.
        /// </summary>
        [SerializeField]
        protected T[] targetComponents;

        public override object TargetComponent => targetComponents;


        #region UnityMethods
        
        #endregion

        
        public override void Dispose ()
        {
            base.Dispose ();
            targetComponents = null;
        }
    }
}