using UnityEngine;

namespace KKSFramework.DataBind
{
    public class MethodBind : Bindable
    {
        #region Methods

        public override void Dispose ()
        {
            targetComponent = null;
            base.Dispose ();
        }

        #endregion


        #region Fields & Property

        [HideInInspector]
        public string methodName;

        /// <summary>
        ///     target comp.
        /// </summary>
        public Component targetComponent;

        public override object BindTarget => BindableExtension.ReturnMethod (targetComponent, methodName);

        #endregion
    }
}