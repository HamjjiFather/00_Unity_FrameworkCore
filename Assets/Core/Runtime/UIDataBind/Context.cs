using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using ModestTree;
using UnityEngine;

namespace KKSFramework.DataBind
{
    [RequireComponent (typeof (IBinder))]
    public class Context : MonoBehaviour, IDisposable
    {
        /// <summary>
        /// 컨테이너에 포함된 컴포넌트 딕셔너리.
        /// </summary>
        public Dictionary<string, Component> Container { get; } = new Dictionary<string, Component> ();

        private bool _isResolved;


        #region UnityMethods

        private void Awake ()
        {
            Resolve ();
        }


        private void OnDestroy ()
        {
            Dispose ();
        }

        #endregion


        public void Resolve (bool isForce = false)
        {
            if (!_isResolved || isForce)
            {
                var classes = GetComponents<IBinder> ();
                classes.Foreach (monoBehaviour =>
                {
                    var fields = monoBehaviour
                        .GetType ()
                        .GetFields (BindingFlags.Instance | BindingFlags.NonPublic)
                        .Where (x => x.HasAttribute<ResolveUIAttribute> ());
                    fields.Foreach (field =>
                    {
                        var attribute = field.GetCustomAttribute<ResolveUIAttribute> (true);
                        field.SetValue (monoBehaviour, Resolve<Component> (attribute.Key));
                    });
                });
            }

            _isResolved = true;
        }


        public void AddComponent (string key, Component target)
        {
            Container.Add (key, target);
        }


        public T Resolve<T> (string key) where T : Component
        {
            if (Vaildate (key))
            {
                return Container[key] as T;
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
    }
}