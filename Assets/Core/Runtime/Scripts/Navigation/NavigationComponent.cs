using KKSFramework.DataBind;
using KKSFramework.Management;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KKSFramework.Navigation
{
    public class NavigationComponent : ComponentBase, IResolveTarget
    {
        #region Fields & Property

        /// <summary>
        ///     root Canvas.
        /// </summary>
        [field: Resolver]
        public Canvas RootCanvas { get; }

        /// <summary>
        ///     pageView Canvas.
        /// </summary>
        [field: Resolver]
        public Transform PageParents { get; }

        /// <summary>
        ///     CommonView Canvas.
        /// </summary>
        [field: Resolver]
        public Transform CommonViewParents { get; }

        /// <summary>
        ///     transition display effector.
        /// </summary>
        [field: Resolver]
        public ViewEffector TransitionEffector { get; }

        /// <summary>
        ///     main camera.
        /// </summary>
        [field: Resolver]
        public Camera MainCamera { get; }

        [field: Resolver]
        public EventSystem RootEventSystem { get; }

        #endregion
    }
}