using UnityEngine;

namespace KKSFramework.DataBind
{
    public class MethodsBind : Bindable
    {
        #region Methods

        public override void Dispose ()
        {
            targetComponents = null;
            base.Dispose ();
        }

        #endregion


        #region Fields & Property

        [HideInInspector]
        public string methodName;

        /// <summary>
        ///     타겟이 되는 컴포넌트.
        /// </summary>
        public Component[] targetComponents;

        /// <summary>
        ///     기준이 되는 타입.
        /// </summary>
        [HideInInspector]
        public Component targetComponent;


        public override object BindTarget =>
            BindableExtension.ReturnMethods (targetComponents, targetComponent, methodName);

        #endregion
    }
}