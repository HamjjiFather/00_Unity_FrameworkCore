using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using UnityEngine;

namespace KKSFramework.Navigation
{
    [RequireComponent (typeof (Context))]
    [RequireComponent (typeof (PopupOption))]
    public class PopupViewBase : ViewBase, IResolveTarget
    {
        #region Fields & Property

        private PopupOption PopupOption => GetCachedComponent<PopupOption> ();

        #endregion


        protected virtual void Awake ()
        {
            PopupOption.InitializePopupOption (ClickClose);
        }


        private void Reset ()
        {
            if (!GetComponent<Context> ())
                gameObject.AddComponent<Context> ();

            if (!GetComponent<PageOption> ())
                gameObject.AddComponent<PopupOption> ();
        }


        #region EventMethods

        protected override UniTask OnPush (object pushValue = null)
        {
            return base.OnPush (pushValue);
        }

        protected override async UniTask OnShow ()
        {
            await PopupOption.ShowAsync (CancellationToken);
            await base.OnShow ();
        }

        protected override async UniTask OnHide ()
        {
            await PopupOption.HideAsync (CancellationToken);
            await base.OnHide ();
        }


        protected virtual void ClickClose ()
        {
            NavigationManager.Instance.GoBackPage ().Forget ();
        }

        #endregion
    }
}