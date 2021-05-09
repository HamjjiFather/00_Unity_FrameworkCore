namespace KKSFramework.DataBind
{
    public class TransformBind : ComponentBind
    {
        #region UnityMethods

        protected override void Reset ()
        {
            base.Reset ();
            targetComponent = transform;
        }

        #endregion


        #region Fields & Property

        #endregion
    }
}