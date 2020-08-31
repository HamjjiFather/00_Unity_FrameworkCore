using System.Threading;
using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using KKSFramework.UI;
using UnityEngine.Events;

namespace KKSFramework.Navigation
{
    public class PopupOption : ViewOption, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649
        
        [Resolver]
        private ButtonExtension _bgButton;
        
        public ButtonExtension BgButton => _bgButton;
        

        [Resolver]
        private ButtonExtension _closeButton;
        
        public ButtonExtension CloseButton => _closeButton;
        

        [Resolver]
        private ViewEffector _viewEffector;

        public ViewEffector ViewEffector => _viewEffector;

#pragma warning restore CS0649

        #endregion
       

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
    }
}