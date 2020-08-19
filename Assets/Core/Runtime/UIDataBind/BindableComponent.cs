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
        private Context _targetContext;
        public Context TargetContext
        {
            get
            {
                if (_targetContext != null) return _targetContext;
                var containerInParents = GetComponentInParent<Context> ();
                if (containerInParents == null)
                {
                    Debug.LogError ("No parents 'Context' Component to bind");
                    return default;
                }

                _targetContext = GetComponentInParent<Context> ();

                return _targetContext;
            }
            private set => _targetContext = value;
        }
        
        /// <summary>
        /// 구분 키 값.
        /// </summary>
        [SerializeField]
        protected string containerPath = string.Empty;
        
        /// <summary>
        /// 타겟이 되는 컴포넌트.
        /// </summary>
        [SerializeField]
        protected Component targetComponent;


        #region UnityMethods

        private void Awake ()
        {
            TargetContext.AddComponent (containerPath, targetComponent);
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
            TargetContext = null;
            targetComponent = null;
        }
    }
}