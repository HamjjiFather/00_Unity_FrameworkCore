using System.Threading;
using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using KKSFramework.UI;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace KKSFramework.Navigation
{
    public class PopupOption : ViewOption, IResolveTarget
    {
        public void InitializePopupOption (UnityAction closeAction)
        {
            BgButton.onClick.AddListener (closeAction);
            CloseButton.onClick.AddListener (closeAction);
        }

        public async UniTask ShowAsync (CancellationToken ct = default)
        {
            await ViewEffector.ShowAsync (ct);
        }

        public async UniTask HideAsync (CancellationToken ct = default)
        {
            await ViewEffector.HideAsync (ct);
        }


        #region Fields & Property

        [field: Resolver]
        public ButtonExtension BgButton { get; }


        [field: Resolver]
        public ButtonExtension CloseButton { get; }


        [field: Resolver]
        public ViewEffector ViewEffector { get; }


        [field: Resolver]
        public Text PopupTitleText { get; }


        [field: Resolver]
        public RectTransform ContentRoot { get; }

        #endregion
    }
}