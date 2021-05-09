using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using UnityEngine;

namespace KKSFramework.Navigation
{
    [RequireComponent (typeof (Context))]
    [RequireComponent (typeof (PageOption))]
    public class PageViewBase : ViewBase, IResolveTarget
    {
        #region UnityMethods

        private void Reset ()
        {
            if (!GetComponent<Context> ())
                gameObject.AddComponent<Context> ();

            if (!GetComponent<PageOption> ())
                gameObject.AddComponent<PageOption> ();
        }

        #endregion


        #region Fields & Property

        /// <summary>
        ///     _pageOption.
        /// </summary>
        private PageOption PageOption => GetCachedComponent<PageOption> ();

        /// <summary>
        ///     Popup stack.
        /// </summary>
        private readonly Stack<PopupViewBase> _registedPopupStack = new Stack<PopupViewBase> ();

        /// <summary>
        ///     Has popup.
        /// </summary>
        public bool ExistPopup => _registedPopupStack.Count != 0;

        #endregion


        #region Methods

        /// <summary>
        ///     페이지 팝업 등록.
        /// </summary>
        public void RegistPopup (PopupViewBase popupViewBase)
        {
            popupViewBase.transform.SetParent (PageOption.PopupParents);
            popupViewBase.RectTransform.SetInstantiateTransform ();
            _registedPopupStack.Push (popupViewBase);
        }

        /// <summary>
        ///     페이지에 오픈된 팝업을 닫음.
        /// </summary>
        /// <returns></returns>
        public async UniTask<bool> CloseLastPopup ()
        {
            if (!ExistPopup) return false;
            var last = _registedPopupStack.Pop ();
            await last.Pop ();
            return true;
        }

        #endregion


        #region EventMethods

        /// <summary>
        ///     뷰 오픈.
        /// </summary>
        protected override async UniTask OnPush (object pushValue = null)
        {
            await UniTask.WhenAll (_registedPopupStack.Select (x => x.Push (pushValue)));
            await base.OnPush (pushValue);
        }

        protected override async UniTask Popped ()
        {
            await UniTask.Run (() => _registedPopupStack.Select (x => x.Pop ()));
            await base.Popped ();
        }

        protected override async UniTask OnForeground ()
        {
            await UniTask.Run (() => _registedPopupStack.Select (x => x.ToForeground ()));
            await base.OnForeground ();
        }

        protected override async UniTask OnBackground ()
        {
            await UniTask.Run (() => _registedPopupStack.Select (x => x.ToBackground ()));
            await base.OnBackground ();
        }

        #endregion
    }
}