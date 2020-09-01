using System.Linq;
using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using UnityEngine;
using UnityEngine.UI;

namespace KKSFramework.Navigation
{
    /// <summary>
    /// 페이지에서 규칙적으로 호출되는 ViewLayout들을 호출하기 위한 컴포넌트.
    /// </summary>
    [RequireComponent(typeof(Context))]
    public class ViewLayoutLoader : MonoBehaviour, IResolveTarget
    {
        #region Fields & Property

        public bool initOnAwake = true;

#pragma warning disable CS0649
        
        [Resolver]
        private ViewLayoutBase[] _viewLayoutObjs;

        public ViewLayoutBase[] ViewLayoutBases => _viewLayoutObjs;

        
        [Resolver]
        private Button[] _layoutViewButton;
        
        public Button[] LayoutViewButton => _layoutViewButton;

#pragma warning restore CS0649

        private int _nowLayout;

        #endregion


        #region UnityMethods

        private void Awake ()
        {
            _layoutViewButton.Foreach ((button, index) =>
            {
                button.onClick.AddListener (() => ClickLayoutViewButton (index));
            });

            if (!initOnAwake) return;
            Initialize ();
        }

        #endregion


        #region Methods

        public void Initialize ()
        {
            _viewLayoutObjs.Foreach (x => { x.Initialize (); });
        }


        public void SetSubView (int index)
        {
            if (_nowLayout >= 0 && _nowLayout < _viewLayoutObjs.Length)
                _viewLayoutObjs[_nowLayout].DisableLayout ().Forget ();
            
            _nowLayout = index;
            _viewLayoutObjs[_nowLayout].ActiveLayout ().Forget ();
        }


        public void CloseViewLayout ()
        {
            _viewLayoutObjs.Foreach (vlo => vlo.DisableLayout ().Forget());
            _nowLayout = -1;
        }

        #endregion


        #region EventMethods

        private void ClickLayoutViewButton (int index)
        {
            SetSubView (index);
        }

        #endregion
    }
}