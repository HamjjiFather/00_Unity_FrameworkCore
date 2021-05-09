using System.Linq;
using UnityEngine;

namespace KKSFramework.DataBind
{
    public abstract class Bindable : MonoBehaviour
    {
        #region Methods

        public virtual void Dispose ()
        {
            // TargetContext = null;
        }

        #endregion


        #region Fields & Property

        /// <summary>
        ///     구분 키 값.
        /// </summary>
        [SerializeField]
        protected string containerPath = string.Empty;

        public string ContainerPath => containerPath;

        public Context TargetContext
        {
            get
            {
                var componentsInParent = GetComponentsInParent<Context> (true);
                if (!componentsInParent.Any ())
                    return default;

                var contextInParents = componentsInParent.FirstOrDefault (x => x.gameObject != gameObject);
                if (contextInParents != null) return contextInParents;
                Debug.LogError ("there is no 'Context' component in parents object to bind");
                return default;
            }
        }

        public abstract object BindTarget { get; }

        #endregion


        #region UnityMethods

        protected virtual void Reset ()
        {
            containerPath = containerPath.Equals (string.Empty) ? gameObject.name : containerPath;
        }

        private void OnDestroy ()
        {
            Dispose ();
        }

        #endregion


        #region EventMethods

        #endregion
    }
}