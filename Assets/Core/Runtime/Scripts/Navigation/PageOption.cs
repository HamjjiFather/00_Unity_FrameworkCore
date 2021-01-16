using KKSFramework.DataBind;
using UnityEngine;

namespace KKSFramework.Navigation
{
    public class PageOption : ViewOption, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private RectTransform _popupParents;

        public RectTransform PopupParents => _popupParents;

        
        [Resolver]
        private RectTransform _contentRoot;

        public RectTransform ContentRoot => _contentRoot;

#pragma warning restore CS0649

        #endregion
    }
}