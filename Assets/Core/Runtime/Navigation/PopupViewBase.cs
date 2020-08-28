using Cysharp.Threading.Tasks;
using UnityEngine;

namespace KKSFramework.Navigation
{
    [RequireComponent (typeof (PopupOption))]
    public class PopupViewBase : ViewBase
    {
        #region Fields & Property

        private PopupOption popupOption => GetCachedComponent<PopupOption> ();

        #endregion

        
        protected virtual void Awake ()
        {
            popupOption.InitializePopupOption (ClickClose);
        }

        
        #region EventMethods

        protected override UniTask OnPush (object pushValue = null)
        {
            return base.OnPush (pushValue);
        }

        protected override async UniTask OnShow ()
        {
            await popupOption.ShowAsync (CancellationToken);
            await base.OnShow ();
        }

        protected override async UniTask OnHide ()
        {
            await popupOption.HideAsync (CancellationToken);
            await base.OnHide ();
        }


        protected virtual void ClickClose ()
        {
            NavigationManager.Instance.GoBackPage ().Forget();
        }

        #endregion
    }
}