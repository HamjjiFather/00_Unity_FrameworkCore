using UnityEngine;

namespace KKSFramework.DataBind
{
    public abstract class Bindable : MonoBehaviour
    {
        #region Fields & Property
        
        /// <summary>
        /// 구분 키 값.
        /// </summary>
        [SerializeField]
        protected string containerPath = string.Empty;
        public string ContainerPath => containerPath;

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

        public abstract object TargetComponent
        {
            get;
        }
        
#pragma warning disable CS0649

#pragma warning restore CS0649

        #endregion


        #region UnityMethods

        protected virtual void Awake ()
        {
            
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


        #region Methods
        
        public virtual void Dispose ()
        {
            TargetContext = null;
            
        }

        #endregion


        #region EventMethods

        #endregion
    }
}