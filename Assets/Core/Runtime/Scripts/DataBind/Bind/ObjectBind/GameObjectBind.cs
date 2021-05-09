namespace KKSFramework.DataBind
{
    public class GameObjectBind : ComponentBind
    {
        #region UnityMethods

        protected override void Reset ()
        {
            base.Reset ();
            targetComponent = gameObject;
        }

        #endregion


        #region Fields & Property

        #endregion
    }
}