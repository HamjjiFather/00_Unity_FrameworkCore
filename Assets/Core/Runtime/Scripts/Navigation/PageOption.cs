using KKSFramework.DataBind;
using UnityEngine;

namespace KKSFramework.Navigation
{
    public class PageOption : ViewOption, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        [Resolver]
        private Transform _popupParents;

        public Transform PopupParents => _popupParents;

#pragma warning restore CS0649

        #endregion
    }
}