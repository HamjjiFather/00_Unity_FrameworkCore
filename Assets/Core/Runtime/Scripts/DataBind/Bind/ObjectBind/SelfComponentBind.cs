namespace KKSFramework.DataBind
{
    public class SelfComponentBind : ComponentBind
    {
        #region UnityMethods

        protected override void Reset ()
        {
            base.Reset ();
            targetComponent = this;
        }

        #endregion
    }
}