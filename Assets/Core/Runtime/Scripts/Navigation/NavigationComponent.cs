using KKSFramework.DataBind;
using KKSFramework.Management;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KKSFramework.Navigation
{
    public class NavigationComponent : ComponentBase, IResolveTarget
    {
        #region Fields & Property

#pragma warning disable CS0649

        /// <summary>
        /// root Canvas.
        /// </summary>
        [Resolver]
        private Canvas _rootCanvas;
        
        public Canvas RootCanvas => _rootCanvas;
        
        /// <summary>
        /// pageView Canvas.
        /// </summary>
        [Resolver]
        private Transform _pageParents;

        public Transform PageParents => _pageParents;

        /// <summary>
        /// CommonView Canvas.
        /// </summary>
        [Resolver]
        private Transform _commonViewParents;

        public Transform CommonViewParents => _commonViewParents;

        /// <summary>
        /// transition display effector.
        /// </summary>
        [Resolver]
        private ViewEffector _transitionEffector;

        public ViewEffector TransitionEffector => _transitionEffector;

        /// <summary>
        /// main camera.
        /// </summary>
        [Resolver]
        private Camera _mainCamera;
        
        public Camera MainCamera => _mainCamera;

        [Resolver]
        private EventSystem _rootEventSystem;

        public EventSystem RootEventSystem => _rootEventSystem;

#pragma warning restore CS0649

        #endregion
    }
}