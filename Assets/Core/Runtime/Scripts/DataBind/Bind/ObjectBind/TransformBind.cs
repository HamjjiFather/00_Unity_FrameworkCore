using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.DataBind
{
    public class TransformBind : ComponentBind
    {
        #region Fields & Property

#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        protected override void Reset ()
        {
            base.Reset ();
            targetComponent = transform;
        }

        #endregion
    }
}