using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace KKSFramework.DataBind
{
    [RequireComponent(typeof(UIBehaviour))]
    public class BindableComponent : MonoBehaviour, IDisposable
    {
        /// <summary>
        /// 컨테이너.
        /// </summary>
        private UIContainer _targetUIContainer;
        public UIContainer TargetUIContainer
        {
            get
            {
                if (_targetUIContainer != null) return _targetUIContainer;
                var containerInParents = GetComponentInParent<UIContainer> ();
                if (containerInParents == null)
                {
                    Debug.LogError ("No parents 'UIContainer' Component to bind");
                    return default;
                }

                _targetUIContainer = GetComponentInParent<UIContainer> ();

                return _targetUIContainer;
            }
            private set => _targetUIContainer = value;
        }
        
        /// <summary>
        /// 구분 키 값.
        /// </summary>
        [SerializeField]
        protected string containerPath = string.Empty;
        
        /// <summary>
        /// 타겟이 되는 컴포넌트.
        /// </summary>
        private UIBehaviour _targetComponent;


        #region UnityMethods

        private void Awake ()
        {
            _targetComponent = GetComponent<UIBehaviour> ();
            TargetUIContainer.AddComponent (containerPath, _targetComponent);
        }


        private void Reset ()
        {
            containerPath = containerPath.Equals (string.Empty) ? gameObject.name : containerPath;
        }
        
        
        private void OnDestroy ()
        {
            Dispose ();
        }
        
        #endregion

        
        public void Dispose ()
        {
            TargetUIContainer = null;
            _targetComponent = null;
        }
    }
}