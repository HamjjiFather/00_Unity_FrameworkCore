using UnityEngine;

namespace KKSFramework.DataBind
{
    public class BindableComponents<T> : Bindable where T : Object
    {
        /// <summary>
        /// 타겟이 되는 컴포넌트.
        /// </summary>
        [SerializeField]
        protected T[] targetComponents;

        public override object TargetComponent => targetComponents;


        public override void Dispose ()
        {
            base.Dispose ();
            targetComponents = null;
        }
    }
}