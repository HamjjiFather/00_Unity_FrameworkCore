using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ModestTree;
using UnityEngine;

namespace KKSFramework.DataBind
{
    public class Context : MonoBehaviour, IDisposable
    {
        /// <summary>
        /// 컨테이너에 포함된 컴포넌트 딕셔너리.
        /// </summary>
        public Dictionary<string, object> Container { get; } = new Dictionary<string, object> ();

        /// <summary>
        /// 자동 Resolve 실행 여부.
        /// </summary>
        public bool autoRun = true;

        /// <summary>
        /// Resolve가 되었는지 여부.
        /// </summary>
        private bool _isResolved;


        #region UnityMethods

        private void Awake ()
        {
            if (autoRun && CheckRunnableState ())
            {
                Resolve ();
            }
        }


        private void OnDestroy ()
        {
            Dispose ();
        }

        #endregion


        private bool CheckRunnableState ()
        {
            var classes = GetComponents<IBinder> ();
            var result = classes.Any ();
            if (!result)
            {
                Debug.LogWarning ($"[{nameof (Context)}] There is no 'IResolve' component in this game object.");
            }

            return classes.Any ();
        }


        /// <summary>
        /// 바인딩된 컴포넌트를 할당한다. 
        /// </summary>
        public void Resolve (bool isForce = false)
        {
            if (CheckRunnableState () && !_isResolved || isForce)
            {
                var bindingComps = GetComponentsInChildren<Bindable> ().Where (x => x.TargetContext.Equals (this));

                var binderClass = GetComponent<IBinder> ();
                var fields = binderClass
                    .GetType ()
                    .GetFields (BindingFlags.Instance | BindingFlags.NonPublic)
                    .Where (x => x.HasAttribute<ResolveUIAttribute> ());

                fields.Foreach (field =>
                {
                    var attribute = field.GetCustomAttribute<ResolveUIAttribute> (true);
                    var bindingComp = bindingComps.Single (x => x.ContainerPath.Equals (attribute.Key));
                    AddComponent (attribute.Key, bindingComp.TargetComponent);
                    field.SetValue (binderClass, bindingComp.TargetComponent);
                });
            }

            _isResolved = true;
        }


        public void AddComponent (string key, object target)
        {
            Container.Add (key, target);
        }


        public object Resolve (string key, Type type)
        {
            if (Vaildate (key))
            {
                return Convert.ChangeType (Container[key], type);
            }

            Debug.Log ($"No UIBehaviour component named {key}.");
            return default;
        }


        public bool Vaildate (string key)
        {
            return Container.ContainsKey (key);
        }


        public void Dispose ()
        {
            Container.Clear ();
            GC.Collect ();
        }

        public void ChangeAutoRunState (bool value)
        {
            autoRun = value;
        }
    }
}