using KKSFramework.DataBind;
using UnityEngine.UI;

namespace KKSFramework.Navigation
{
    /// <summary>
    ///     페이지에서 규칙적으로 호출되는 ViewLayout들을 호출하기 위한 컴포넌트.
    /// </summary>
    public class ViewLayoutLoaderWithButton : ViewLayoutLoaderBase
    {
        #region UnityMethods

        protected override void Awake ()
        {
            LayoutViewButton?.Foreach ((button, index) =>
            {
                button.onClick.AddListener (() => ClickLayoutViewButton (index));
            });
            base.Awake ();
        }

        #endregion


        #region EventMethods

        private void ClickLayoutViewButton (int index)
        {
            SetSubView (index);
        }

        #endregion


        #region Fields & Property

        [field: Resolver]
        public Button[] LayoutViewButton { get; }


        private int _nowLayout;

        #endregion


        #region Methods

        #endregion
    }
}