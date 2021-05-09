using Cysharp.Threading.Tasks;
using KKSFramework.DataBind;
using UnityEngine;

namespace KKSFramework.Navigation
{
    /// <summary>
    ///     페이지에서 규칙적으로 호출되는 ViewLayout들을 호출하기 위한 컴포넌트.
    /// </summary>
    [RequireComponent (typeof (Context))]
    public class ViewLayoutLoaderBase : MonoBehaviour, IResolveTarget
    {
        #region UnityMethods

        protected virtual void Awake ()
        {
            if (!initOnAwake) return;
            Initialize ();
        }

        #endregion


        #region Fields & Property

        public bool initOnAwake = true;


        [field: Resolver]
        public ViewLayoutBase[] ViewLayoutBases { get; }


        private int _nowLayout;

        #endregion


        #region Methods

        public virtual void Initialize ()
        {
            ViewLayoutBases.Foreach (x => { x.Initialize (); });
        }


        public virtual void SetSubView (int index)
        {
            if (_nowLayout >= 0 && _nowLayout < ViewLayoutBases.Length)
                ViewLayoutBases[_nowLayout].DisableLayout ().Forget ();

            _nowLayout = index;
            ViewLayoutBases[_nowLayout].ActiveLayout ().Forget ();
        }


        public virtual void CloseViewLayout ()
        {
            ViewLayoutBases.Foreach (vlo => vlo.DisableLayout ().Forget ());
            _nowLayout = -1;
        }

        #endregion
    }
}