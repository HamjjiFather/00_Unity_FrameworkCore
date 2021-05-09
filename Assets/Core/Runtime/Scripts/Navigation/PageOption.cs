using KKSFramework.DataBind;
using UnityEngine;

namespace KKSFramework.Navigation
{
    public class PageOption : ViewOption, IResolveTarget
    {
        #region Fields & Property

        [field: Resolver]
        public RectTransform PopupParents { get; }


        [field: Resolver]
        public RectTransform ContentRoot { get; }

        #endregion
    }
}